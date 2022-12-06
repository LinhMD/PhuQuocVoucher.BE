using CrudApiTemplate.View;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ComboDto;

public class ComboSView : BaseModel, IView<Voucher>, IDto
{
    public int Id { get; set; }
    
    public string VoucherName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string?  SocialPost { get; set; }

    public int Inventory { get; set; }
    
    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public long SoldPrice { get; set; }

}