using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.PriceBookDto;

public class CreatePriceBookSimple  : CreateDto, ICreateRequest<PriceBook>
{
    
    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Only positive number allowed")]
    public double Price { get; set; }
}