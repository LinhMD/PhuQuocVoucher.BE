using System.Linq.Expressions;
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
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Business.Dtos.ProductDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ComboService : ServiceCrud<Combo>, IComboService
{
    private ILogger<ComboService> _logger;
    
    private readonly IProductService _productService;

    private readonly IVoucherService _voucherService;

    private readonly IUnitOfWork _work;
    public ComboService(IUnitOfWork work, ILogger<ComboService> logger, IProductService productService, IVoucherService voucherService) : base(work.Get<Combo>(), work, logger)
    {
        _work = work;
        _logger = logger;
        _productService = productService;
        _voucherService = voucherService;
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

    public async Task<(IList<ComboView>, int)> FindComboAsync(GetRequest<Combo> request, Role? role)
    {
        var queryable = Repository.Find(request.FindRequest.ToPredicate());

        var total = queryable.Count();

        Expression<Func<Combo, ComboView>> select = role == Role.Customer?
            c => new ComboView //customer can't see seller price :v 
            {
            Id = c.Id,
            Name = c.Name,
            EndDate = c.EndDate,
            StartDate = c.StartDate,
            ProductId = c.ProductId,
            Vouchers = c.Vouchers.Select(v => new VoucherSView()
            {
                Id = v.Id,
                Inventory = v.Inventory,
                EndDate = v.EndDate,
                StartDate = v.StartDate,
                ProductId = v.ProductId,
                VoucherName = v.VoucherName,
                ServiceId = v.ServiceId,
                LimitPerDay = v.LimitPerDay,
                IsRequireProfileInfo = v.IsRequireProfileInfo
            }),
            Content = c.Product.Content,
            Description = c.Product.Description,
            Prices = c.Product.Prices.Where(p => !p.IsSellerPrice).Select(price => new PriceBookSView()
            {
                Id = price.Id,
                Price = price.Price,
                PriceLevelName = price.PriceLevel.Name
            }),
            Summary = c.Product.Summary,
            Type = c.Product.Type,
            BannerImg = c.Product.BannerImg,
            IsForKid = false
        } : 
            c => new ComboView
            {
                Id = c.Id,
                Name = c.Name,
                EndDate = c.EndDate,
                StartDate = c.StartDate,
                ProductId = c.ProductId,
                Vouchers = c.Vouchers.Select(v => new VoucherSView()
                {
                    Id = v.Id,
                    Inventory = v.Inventory,
                    EndDate = v.EndDate,
                    StartDate = v.StartDate,
                    ProductId = v.ProductId,
                    VoucherName = v.VoucherName,
                    ServiceId = v.ServiceId,
                    LimitPerDay = v.LimitPerDay,
                    IsRequireProfileInfo = v.IsRequireProfileInfo
                }),
                Content = c.Product.Content,
                Description = c.Product.Description,
                Prices = c.Product.Prices.Select(price => new PriceBookSView()
                {
                    Id = price.Id,
                    Price = price.Price,
                    PriceLevelName = price.PriceLevel.Name
                }),
                Summary = c.Product.Summary,
                Type = c.Product.Type,
                BannerImg = c.Product.BannerImg,
                IsForKid = false
            } ;
        var result = await queryable
            .OrderBy(request.OrderRequest)
            .Paging(request.GetPaging())
            .Select(select).ToListAsync();
        
        return (result, total);
        
        
    }
}