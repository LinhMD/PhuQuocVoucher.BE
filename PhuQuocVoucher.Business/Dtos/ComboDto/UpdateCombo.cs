using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ComboDto;

public class UpdateCombo : UpdateDto, IUpdateRequest<Voucher>
{
    public string? VoucherName { get; set; }

    public string? BannerImg { get; set; }

    public string? Description { get; set; }

    public string? Summary { get; set; }
    
    public string?  SocialPost { get; set; }
    
    public double? SellPrice { get; set; }

    public string? Content { get; set; }
    
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public ModelStatus? Status { get; set; }
}