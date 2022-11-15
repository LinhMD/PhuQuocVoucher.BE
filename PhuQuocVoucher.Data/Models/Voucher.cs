
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(VoucherName), IsUnique = true)]
public class Voucher : BaseModel
{

    public int Id { get; set; }
    public string VoucherName { get; set; }

    public int? LimitPerDay { get; set; }

    public bool? IsRequireProfileInfo { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Service Service { get; set; }

    public int ServiceId { get; set; }

    public IList<QrCodeInfo> QrCodeInfos { get; set; }
    
    public int SlotNumber { get; set; }

    public int ProviderId { get; set; }

    public ServiceProvider Provider { get; set; }
    
    public string? Description { get; set; }

    public string? Summary { get; set; }

    public int Inventory { get; set; }

    public double? DisplayPrice { get; set; }
    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public IEnumerable<PriceBook> Prices { get; set; }
    
    public IEnumerable<Tag> Tags { get; set; }

    public IEnumerable<Review> Reviews { get; set; }

}