using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Business.Dtos.TagDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProductDto;

public class ProductView : IView<Product>, IDto
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public bool IsForKid { get; set; }

    public ProductType Type { get; set; }
    
    public int Inventory { get; set; }
    
    public IEnumerable<PriceBookSView> Prices { get; set; }
    
    
    public IEnumerable<TagView> Tags { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Product, ProductView>.NewConfig();

    }
}