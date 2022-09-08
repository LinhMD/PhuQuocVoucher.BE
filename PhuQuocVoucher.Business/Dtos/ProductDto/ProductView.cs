using CrudApiTemplate.View;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProductDto;

public class ProductView : IView<Product>, IDto
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public double Price { get; set; }

    public bool IsForKid { get; set; }

    public ProductType Type { get; set; }
}