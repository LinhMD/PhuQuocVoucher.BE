using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class UpdateVoucher : UpdateDto, IUpdateRequest<Voucher>
{
    public string? VoucherName { get; set; }

    public string? BannerImg { get; set; }

    public string? Description { get; set; }

    public string? Summary { get; set; }
    
    public long? SoldPrice { get; set; }

    public string? Content { get; set; }
    
    public string?  SocialPost { get; set; }
    
    public long? VoucherValue { get; set; }

    public IList<int>? TagIds { get; set; }
    
    public float? CommissionRate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public ModelStatus? Status { get; set; }
}