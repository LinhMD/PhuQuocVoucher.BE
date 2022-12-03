using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class CreateVoucher : CreateDto, ICreateRequest<VoucherCompaign>
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
    
    public double? DisplayPrice { get; set; }
    
    public string? BannerImg { get; set; }
    
    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string? Content { get; set; }

    public IList<int> TagIds { get; set; }

    public double AdultPrice { get; set; }

    public double ChildrenPrice { get; set; }
    
}