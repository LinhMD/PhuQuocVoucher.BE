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
            var voucherView = (await UnitOfWork.Get<Voucher>().AddAsync(voucher)).Adapt<VoucherView>();
            return await UnitOfWork.Get<Voucher>().Find<VoucherView>(v => v.Id == voucherView.Id).FirstOrDefaultAsync() ?? voucherView;
        }
        catch (Exception e)
        {
            throw new DbQueryException(e.Message, DbError.Create);
        }
    }
    
    
}