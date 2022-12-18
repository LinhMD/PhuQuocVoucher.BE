using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class CreateVoucher : CreateDto, ICreateRequest<Voucher>
{
    [Required] public string VoucherName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required] public int ServiceId { get; set; }

    public int? ProviderId { get; set; }

    public string? BannerImg { get; set; }

    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string? Content { get; set; }
    
    public string?  SocialPost { get; set; }

    public float CommissionRate { get; set; }
    public IList<int> TagIds { get; set; }

    public long SoldPrice { get; set; }

    public long VoucherValue { get; set; }

    public bool IsCombo { get; set; }
    
    public int Inventory { get; set; }
}