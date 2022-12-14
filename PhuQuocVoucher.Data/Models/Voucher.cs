
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class Voucher : BaseModel, IOrderAble
{
    public int Id { get; set; }
    
    public string VoucherName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Service? Service { get; set; }
    
    public int? ServiceId { get; set; }

    public ServiceType? ServiceType { get; set; }

    public int? ServiceTypeId { get; set; }
    
    public int? ProviderId { get; set; }

    public ServiceProvider? Provider { get; set; }
    
    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string? BannerImg { get; set; }

    public string? Content { get; set; }
    
    public string?  SocialPost { get; set; }
    
    public bool IsCombo { get; set; }

    public long SoldPrice { get; set; }
    
    public int Inventory { get; set; }

    public long VoucherValue { get; set; }
    
    public float CommissionRate { get; set; }
    
    public IList<VoucherTag> Tags { get; set; }

    public IList<Review> Reviews { get; set; }
    
    public IList<QrCode> QrCodes { get; set; }

    public IList<ComboVoucher> Combos { get; set; }

    public IList<ComboVoucher> Vouchers { get; set; }

    public IList<OrderItem> OrderItems { get; set; }

    public void ConfigOrderBy()
    {
        SetUpOrderBy<Voucher>();
        Expression<Func<Voucher, DateTime?>> startDate = voucher => voucher.StartDate;
        OrderByProvider<Voucher>.OrderByDic.Add(nameof(StartDate), startDate);
        Expression<Func<Voucher, DateTime?>> endDate = voucher => voucher.EndDate;
        OrderByProvider<Voucher>.OrderByDic.Add(nameof(EndDate), endDate);
    }
}