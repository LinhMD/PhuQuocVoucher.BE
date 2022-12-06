using System.Linq.Expressions;
using System.Text.Json.Serialization;
using CrudApiTemplate.OrderBy;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(HashCode), IsUnique = true)]
public class QrCode : BaseModel, IOrderAble
{
    public int Id { get; set; }
    
    public string HashCode { get; set; }
    
    [JsonIgnore]
    public Voucher Voucher { get; set; }

    public int VoucherId { get; set; }
    
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    public int? ProviderId { get; set; }
    
    [JsonIgnore]
    public ServiceProvider? Provider { get; set; }

    public Service Service { get; set; }

    public long SoldPrice { get; set; }

    public int ServiceId { get; set; }

    public QrCodeStatus QrCodeStatus { get; set; }
    
    public DateTime? UseDate { get; set; }

    public Order? Order { get; set; }

    public int? OrderId { get; set; }
    
    public void ConfigOrderBy()
    {
        SetUpOrderBy<QrCode>();
        Expression<Func<QrCode, QrCodeStatus>> orderByStatus = qr => qr.QrCodeStatus;
        OrderByProvider<QrCode>.OrderByDic.Add(nameof(QrCodeStatus),orderByStatus);
    }
}