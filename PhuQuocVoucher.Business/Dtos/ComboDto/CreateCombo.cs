using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Business.Dtos.ProductDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ComboDto;

public class CreateCombo : CreateDto, ICreateRequest<Combo>
{
    [Required]
    public string Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public CreateProduct CreateProduct { get; set; }

    [Required]
    public IEnumerable<int> VoucherIds { get; set; }

}