using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Api.Ultility;
using PhuQuocVoucher.Business.Dtos.ComboDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ComboService : ServiceCrud<Voucher>, IComboService
{
    public ComboService(IUnitOfWork  work, ILogger<ComboService> logger) : base(work.Get<Voucher>(), work, logger)
    {
    }
    public async Task<ComboView> UpdateTag(IList<int> tagIds, int voucherId)
    {
        var voucher = await Repository.Find(v => v.Id == voucherId).FirstOrDefaultAsync();
        if (voucher == null) throw new ModelNotFoundException($"Combo not found with id {voucherId}");

        await UnitOfWork.Get<VoucherTag>().RemoveAllAsync(voucher.Tags);
        var tags = await UnitOfWork.Get<Tag>().Find(t => tagIds.Contains(t.Id)).ToListAsync();

        var tagVouchers = tags.Select(t => new VoucherTag(){TagId = t.Id, VoucherId = voucherId}).ToList();
        await UnitOfWork.Get<VoucherTag>().AddAllAsync(tagVouchers);

        voucher.Tags = tagVouchers;
        return voucher.Adapt<ComboView>();
    }

    public async Task<ComboView> CreateCombo(CreateCombo createCombo)
    {
        try
        {
            var combo = (createCombo as ICreateRequest<Voucher>).CreateNew(UnitOfWork);

            var vouchers = await UnitOfWork.Get<Voucher>().Find(v => createCombo.VoucherIds.Contains(v.Id)).ToListAsync();

            var beginDateMax = vouchers.Select(v => v.StartDate).Max();
            var endDateMin = vouchers.Select(v => v.EndDate).Min();
            var minInventory = vouchers.Select(v => v.Inventory).Min();
            
            if (combo.StartDate < beginDateMax || combo.EndDate > endDateMin)
            {
                throw new ModelValueInvalidException("Combo effective date invalid");
            }
            
            if (combo.Inventory > minInventory )
            {
                throw new ModelValueInvalidException("Combo inventory invalid");
            }
            
            var tags = await UnitOfWork.Get<Tag>().Find(t => createCombo.TagIds.Contains(t.Id)).ToListAsync();
            
            await UnitOfWork.Get<Voucher>().AddAsync(combo);
            
            var tagVouchers = tags.Select(t => new VoucherTag(){TagId = t.Id, VoucherId = combo.Id}).ToList();
            await UnitOfWork.Get<VoucherTag>().AddAllAsync(tagVouchers);

            combo.Tags = tagVouchers;
            
            
            var comboVouchers = createCombo.VoucherIds.Select(v => new ComboVoucher(){ComboId = combo.Id, VoucherId = v}).ToList();
            await UnitOfWork.Get<ComboVoucher>().AddAllAsync(comboVouchers);

            return await UnitOfWork.Get<Voucher>().Find<ComboView>(v => v.Id == combo.Id).FirstOrDefaultAsync() ?? throw new ModelNotFoundException("how?");
        }
        catch (Exception e)
        {
            
            e.StackTrace.Dump();
            throw new DbQueryException(e.Message, DbError.Create);
        }
        
    }

}