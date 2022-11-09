using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class CreateVoucher : CreateDto, ICreateRequest<Voucher>
{
    [Required]
    public string VoucherName { get; set; }

    public int? LimitPerDay { get; set; }

    [Required]
    public bool IsRequireProfileInfo { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    [Required]
    public int ServiceId { get; set; }

    [Required]
    public int ProviderId { get; set; }

    [Required]
    [Range(1, 20)]
    public int SlotNumber { get; set; }

    public string? BannerImg { get; set; }
}