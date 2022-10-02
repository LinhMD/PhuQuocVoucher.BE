using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProductDto;

public class FindProduct : IFindRequest<Product>
{
    public int? Id { get; set; }

    [Contain]
    public string? Description { get; set; }

    [Contain]
    public string? Summary { get; set; }
    [Contain]
    public string? Content { get; set; }

    public bool? IsForKid { get; set; }

    public ProductType? Type { get; set; }
}