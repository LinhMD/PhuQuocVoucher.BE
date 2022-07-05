using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Dtos.VoucherDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class VoucherService : ServiceCrud<Voucher>, IVoucherService
{
    public VoucherService(IUnitOfWork work) : base(work.Get<Voucher>(), work)
    {
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