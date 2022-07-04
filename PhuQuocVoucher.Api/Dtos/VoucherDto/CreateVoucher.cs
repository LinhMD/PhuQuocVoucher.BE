using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Api.Dtos.ProductDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.VoucherDto;

public class CreateVoucher : CreateDto, ICreateRequest<Voucher>
{
    [Required]
    public string VoucherName { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public int Inventory { get; set; }

    public int? LimitPerDay { get; set; }

    [Required]
    public bool IsRequireProfileInfo { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required]
    public CreateProduct CreateProduct { get; set; }

    [Required]
    public int ServiceId { get; set; }

}