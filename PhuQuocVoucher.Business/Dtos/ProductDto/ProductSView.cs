using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProductDto;

public class ProductSView :IView<Product>, IDto
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public double Price { get; set; }
    
    public ProductType Type { get; set; }
    
    public void InitMapper()
    {
        TypeAdapterConfig<Product, ProductSView>.NewConfig().Map(view => view.Price, product => product.Prices.FirstOrDefault(p => p.IsDefault).Price);
    }
}