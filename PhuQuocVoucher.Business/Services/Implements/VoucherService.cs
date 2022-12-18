using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Api.Ultility;
using PhuQuocVoucher.Business.Dtos.ComboDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Business.Services.Implements;

public class VoucherService : ServiceCrud<Voucher>, IVoucherService
{
    private ILogger<VoucherService> _logger;

    public VoucherService(IUnitOfWork work, ILogger<VoucherService> logger) : base(work.Get<Voucher>(), work, logger)
    {
        _logger = logger;
    }

    public async Task<VoucherView> CreateAsync(CreateVoucher createVoucher)
    {
        try
        {
            var voucher = (createVoucher as ICreateRequest<Voucher>).CreateNew(UnitOfWork);
            voucher.Status = ModelStatus.New;
            var serviceTypeId = await UnitOfWork.Get<Service>().Find(service =>  service.Id == createVoucher.ServiceId).Select(s => s.ServiceTypeId).FirstOrDefaultAsync();
            voucher.ServiceTypeId = serviceTypeId;
            var service = await UnitOfWork.Get<ServiceType>().Find(service => service.Id == serviceTypeId).FirstOrDefaultAsync();
            
            if (service?.DefaultCommissionRate > voucher.CommissionRate)
                throw new ModelValueInvalidException($"Tỉ lệ hoa hồng thấp nhất của dịch vụ {service.Name} là {service.DefaultCommissionRate}");
            
            var tags = await UnitOfWork.Get<Tag>().Find(t => createVoucher.TagIds.Contains(t.Id)).ToListAsync();

            await UnitOfWork.Get<Voucher>().AddAsync(voucher);
            
            var qrCodeInfos = Enumerable.Range(0, voucher.Inventory).Select(qr => new QrCode()
            {
                QrCodeStatus = QrCodeStatus.Active,
                CreateAt = DateTime.Now,
                HashCode = Guid.NewGuid().ToString(),
                ProviderId = ((int) voucher.ProviderId)!,
                VoucherId = voucher.Id ,
                ServiceId = voucher.ServiceId ?? 0,
                Status = ModelStatus.Active,
                EndDate = voucher.EndDate ?? DateTime.Now,
                StartDate = voucher.StartDate ?? DateTime.Now
            });

            await UnitOfWork.Get<QrCode>().AddAllAsync(qrCodeInfos);

            var tagVouchers = tags.Select(t => new VoucherTag(){TagId = t.Id, VoucherId = voucher.Id}).ToList();
            await UnitOfWork.Get<VoucherTag>().AddAllAsync(tagVouchers);

            voucher.Tags = tagVouchers;
            return await UnitOfWork.Get<Voucher>().Find<VoucherView>(v => v.Id == voucher.Id).FirstOrDefaultAsync() ?? throw new ModelNotFoundException("how?");
        }
        catch (Exception e)
        {
            
            e.StackTrace.Dump();
            throw new DbQueryException(e.Message, DbError.Create);
        }
    }
    
    public async Task<VoucherView> UpdateVoucher(UpdateVoucher updateVoucher, int id)
    {
        try
        {
            var voucher = await UnitOfWork.Get<Voucher>().Find(v => v.Id == id).FirstOrDefaultAsync() ?? throw new ModelNotFoundException($"voucher not found with id {id}");

            var serviceType = await UnitOfWork.Get<ServiceType>().Find(service => service.Id == voucher.ServiceTypeId).FirstOrDefaultAsync();
            
            if (serviceType?.DefaultCommissionRate > updateVoucher.CommissionRate)
                throw new ModelValueInvalidException($"Tỉ lệ hoa hồng thấp nhất của dịch vụ {serviceType.Name} là {serviceType.DefaultCommissionRate}");
            
            (updateVoucher as IUpdateRequest<Voucher>).UpdateModel(ref voucher, UnitOfWork);
            
            if (updateVoucher.TagIds != null)
            {
                await UpdateTag(updateVoucher.TagIds, voucher.Id);
            }
            
            voucher.Validate();
            await UnitOfWork.CompleteAsync();
            
            return await UnitOfWork.Get<Voucher>().Find<VoucherView>(v => v.Id == id).FirstOrDefaultAsync() ?? throw new ModelNotFoundException($"voucher not found with id {id}");
        }
        catch (Exception e)
        {
            e.StackTrace.Dump();
            throw new DbQueryException(e.Message, DbError.Create);
        }
    }

    public async Task<VoucherView> UpdateTag(IList<int> tagIds, int voucherId)
    {
        var voucher = await Repository.Find(v => v.Id == voucherId).FirstOrDefaultAsync();
        if (voucher == null) throw new ModelNotFoundException($"Voucher not found with id {voucherId}");

        await UnitOfWork.Get<VoucherTag>().RemoveAllAsync(voucher.Tags);
        var tags = await UnitOfWork.Get<Tag>().Find(t => tagIds.Contains(t.Id)).ToListAsync();

        var tagVouchers = tags.Select(t => new VoucherTag(){TagId = t.Id, VoucherId = voucherId}).ToList();
        await UnitOfWork.Get<VoucherTag>().AddAllAsync(tagVouchers);

        voucher.Tags = tagVouchers;
        return voucher.Adapt<VoucherView>();
    }

    public async Task UpdateVoucherInventoryList(IList<int> voucherIds, Dictionary<int, int>? comboQuantity = null)
    {
        try
        {
            await (Repository as IVoucherRepository)?.UpdateVoucherInventoryList(voucherIds)!;

            var combos = await Repository.Find<ComboView>(v => voucherIds.Contains(v.Id) && v.IsCombo).ToListAsync();
            var dictionary = combos.ToDictionary(
                    g => g.Id, 
                    g =>  g.Vouchers.Select(v => v.Inventory).Min());
            var realCombos = await Repository.Find(v => voucherIds.Contains(v.Id) && v.IsCombo).ToListAsync();

            foreach (var combo in realCombos)
            {
                if (comboQuantity == null) continue;
                var newInventory = combo.Inventory - comboQuantity[combo.Id];
                combo.Inventory = dictionary[combo.Id] < newInventory ? dictionary[combo.Id] : newInventory;

            }
            await Repository.CommitAsync();
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Update inventory fail");
        }

    }
    public async Task UpdateInventory()
    {
        try
        {
            await (Repository as IVoucherRepository)?.UpdateVoucherInventory()!;
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Update inventory fail");
        }

    }

}