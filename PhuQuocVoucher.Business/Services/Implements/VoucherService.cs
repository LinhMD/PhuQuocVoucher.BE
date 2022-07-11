using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
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

    public async Task<Voucher> CreateAsync(CreateVoucher createVoucher)
    {
        try
        {
            var productCreate = (createVoucher.CreateProduct as ICreateRequest<Product>).CreateNew(UnitOfWork);
            productCreate.Type = ProductType.Voucher;
            var product = await UnitOfWork.Get<Product>().AddAsync(productCreate);

            var voucher = (createVoucher as ICreateRequest<Voucher>).CreateNew(UnitOfWork);
            voucher.Product = product;

            return await UnitOfWork.Get<Voucher>().AddAsync(voucher);
        }
        catch (Exception e)
        {
            throw new DbQueryException(e.Message, DbError.Create);
        }
    }
}