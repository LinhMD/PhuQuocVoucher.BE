using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProductDto;

public class CreateProduct : CreateDto, ICreateRequest<Product>
{

    [Required]
    public string Description { get; set; }

    [Required]
    public string Summary { get; set; }

    [Required]
    public string BannerImg { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public double Price { get; set; }

    [Required] public bool IsForKid { get; set; } = false;

}