using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using Mapster;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class VoucherService : ServiceCrud<Voucher>, IVoucherService
{
    private ILogger<VoucherService> _logger;

    private IProductService _productService;
    public VoucherService(IUnitOfWork work, ILogger<VoucherService> logger, IProductService productService) : base(work.Get<Voucher>(), work, logger)
    {
        _logger = logger;
        _productService = productService;
    }

    public async Task<VoucherView> CreateAsync(CreateVoucher createVoucher)
    {
        try
        {
            createVoucher.CreateProduct.Type = ProductType.Voucher;
            var product = await _productService.CreateProductAsync(createVoucher.CreateProduct);

            var voucher = (createVoucher as ICreateRequest<Voucher>).CreateNew(UnitOfWork);
            voucher.ProductId = product.Id;
            var voucherView = (await UnitOfWork.Get<Voucher>().AddAsync(voucher)).Adapt<VoucherView>();
            voucherView.Product = product;
            return voucherView;
        }
        catch (Exception e)
        {
            throw new DbQueryException(e.Message, DbError.Create);
        }
    }
}