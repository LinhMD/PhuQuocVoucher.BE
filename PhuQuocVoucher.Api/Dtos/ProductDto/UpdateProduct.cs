using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.ProductDto;

public class UpdateProduct : IUpdateRequest<Product>
{
    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public double? Price { get; set; }

    public bool? IsForKid { get; set; } = false;

}