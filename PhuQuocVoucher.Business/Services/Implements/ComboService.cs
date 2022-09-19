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
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ComboService : ServiceCrud<Combo>, IComboService
{
    private ILogger<ComboService> _logger;
    
    private readonly IProductService _productService;
    public ComboService(IUnitOfWork work, ILogger<ComboService> logger, IProductService productService) : base(work.Get<Combo>(), work, logger)
    {
        _logger = logger;
        _productService = productService;
    }

    public async Task<ComboView> CreateAsync(CreateCombo createCombo)
    {
        try
        {
            createCombo.CreateProduct.Type = ProductType.Voucher;
            var product = await _productService.CreateProductAsync(createCombo.CreateProduct);
            var combo = (createCombo as ICreateRequest<Combo>).CreateNew(UnitOfWork);
            combo.ProductId = product.Id;

            var voucherList = await UnitOfWork.Get<Voucher>().Find(v => createCombo.VoucherIds.Contains(v.Id)).ToListAsync();
            combo.Vouchers = voucherList;
            combo.Validate();
            var comboView = (await UnitOfWork.Get<Combo>().AddAsync(combo)).Adapt<ComboView>();
            comboView.Product = product;
            return comboView;
        }
        catch (DbUpdateException e)
        {
            e.InnerException?.Message.Dump();
            e.InnerException?.StackTrace.Dump();
            throw new DbQueryException(e.Message, DbError.Create);
        }
    }
}