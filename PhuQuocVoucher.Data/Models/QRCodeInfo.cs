using System.Linq.Expressions;
using System.Text.Json.Serialization;
using CrudApiTemplate.OrderBy;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(HashCode), IsUnique = true)]
public class QrCodeInfo : BaseModel,  IOrderAble
{
    public int Id { get; set; }
    
    [JsonIgnore]
    public string HashCode { get; set; }
    
    [JsonIgnore]
    public Voucher Voucher { get; set; }
    
    public string? ImgLink { get; set; }
    
    public int VoucherId { get; set; }

    public QRCodeStatus Status { get; set; } = QRCodeStatus.Active;
    public void ConfigOrderBy()
    {
        Expression<Func<QrCodeInfo, QRCodeStatus>> orderByStatus = qr => qr.Status;
        OrderByProvider<QrCodeInfo>.OrderByDic.Add(nameof(Status),orderByStatus);
    }
}