using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProductDto;

public class CreateProduct : CreateDto, ICreateRequest<Product>
{

    [Required]
    public string Description { get; set; }

    [Required]
    public string Summary { get; set; }
    
    [Required]
    public int Inventory { get; set; }

    [Required]
    public string BannerImg { get; set; }

    [Required]
    public string Content { get; set; }
    public ProductType Type { get; set; }

    public IList<CreatePriceBookSimple> PriceBooks { get; set; }
}