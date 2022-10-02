using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.ProductDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IProductService : IServiceCrud<Product>
{
    public Task<ProductView> CreateProductAsync(CreateProduct createProduct);

    public Task<ProductView> AddTagsAsync(IList<string> tags, int productId);
}