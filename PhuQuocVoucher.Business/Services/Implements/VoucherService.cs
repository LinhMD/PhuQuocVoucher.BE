using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

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
            var tags = await UnitOfWork.Get<Tag>().Find(t => createVoucher.TagIds.Contains(t.Id)).ToListAsync();

            var tagVouchers = tags.Select(t => new TagVoucher(){TagId = t.Id, VoucherId = voucher.Id}).ToList();
            await UnitOfWork.Get<TagVoucher>().AddAllAsync(tagVouchers);

            voucher.Tags = tagVouchers;

            var voucherView = (await UnitOfWork.Get<Voucher>().AddAsync(voucher)).Adapt<VoucherView>();
            return await UnitOfWork.Get<Voucher>().Find<VoucherView>(v => v.Id == voucherView.Id).FirstOrDefaultAsync() ?? voucherView;
        }
        catch (Exception e)
        {
            throw new DbQueryException(e.Message, DbError.Create);
        }
    }

    public async Task<VoucherView> UpdateTag(IList<int> tagIds, int voucherId)
    {
        var voucher = await Repository.Find(v => v.Id == voucherId).FirstOrDefaultAsync();
        if (voucher == null) throw new ModelNotFoundException($"Voucher not found with id {voucherId}");

        await UnitOfWork.Get<TagVoucher>().RemoveAllAsync(voucher.Tags);
        var tags = await UnitOfWork.Get<Tag>().Find(t => tagIds.Contains(t.Id)).ToListAsync();

        var tagVouchers = tags.Select(t => new TagVoucher(){TagId = t.Id, VoucherId = voucherId}).ToList();
        await UnitOfWork.Get<TagVoucher>().AddAllAsync(tagVouchers);

        voucher.Tags = tagVouchers;
        return voucher.Adapt<VoucherView>();
    }
}