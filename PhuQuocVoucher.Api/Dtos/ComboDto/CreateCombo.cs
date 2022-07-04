using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Api.Dtos.ProductDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.ComboDto;

public class CreateCombo : CreateDto, ICreateRequest<Combo>
{
    [Required]
    public string Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public CreateProduct ProductCreate { get; set; }

    [Required]
    public IEnumerable<int> VoucherIds { get; set; }

}