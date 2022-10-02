using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.ProductDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ProductService : ServiceCrud<Product>, IProductService
{
    private ILogger<ProductService> _logger;

    private ITagService _tagService;
    public ProductService(IUnitOfWork work, ILogger<ProductService> logger, ITagService tagService) : base(work.Get<Product>(), work, logger)
    {
        _logger = logger;
        _tagService = tagService;
    }

    public async Task<ProductView> CreateProductAsync(CreateProduct createProduct)
    {
        createProduct.Validate();
        var product = createProduct.Adapt<Product>();
        
        await Repository.AddAsync(product);

        var priceBooks = createProduct.PriceBooks.Select(p => new PriceBook()
        {
            Price = p.Price,
            ProductId = product.Id,
            PriceLevelId = p.PriceLevelId
        }).ToList();
        
        await UnitOfWork.Get<PriceBook>().AddAllAsync(priceBooks);
        
        return (await Repository.Find<ProductView>(p => p.Id == product.Id).FirstOrDefaultAsync())!;
    }

    public async Task<ProductView> AddTagsAsync(IList<string> tags, int productId)
    {
        var foundTags = await _tagService.GetTagsAsync(tags);
        var product = await Repository.Find(product => product.Id == productId).FirstOrDefaultAsync();
        if (product == null) throw new ModelNotFoundException($"Product Id {productId} Not Found!!");
        foreach (var foundTag in foundTags)
        {
            foundTag.Products.Add(product);
        }
        await Repository.CommitAsync();
        var view = await Repository.Find<ProductView>(p => p.Id == productId).FirstOrDefaultAsync();
        view!.Tags = foundTags;
        return view;
    }
}