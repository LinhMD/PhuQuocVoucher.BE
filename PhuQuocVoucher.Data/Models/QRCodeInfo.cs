using System.Linq.Expressions;
using System.Text.Json.Serialization;
using CrudApiTemplate.OrderBy;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(HashCode), IsUnique = true)]
public class QrCodeInfo : BaseModel,  IOrderAble
{
    public int Id { get; set; }
    
    public string HashCode { get; set; }
    
    [JsonIgnore]
    public VoucherCompaign VoucherCompaign { get; set; }
    
    public string? ImgLink { get; set; }
    
    public int VoucherId { get; set; }

    public int? ProviderId { get; set; }

    public ServiceProvider? Provider { get; set; }

    public QRCodeStatus QrStatus { get; set; } = QRCodeStatus.Active;
    public void ConfigOrderBy()
    {
        SetUpOrderBy<QrCodeInfo>();
        Expression<Func<QrCodeInfo, QRCodeStatus>> orderByStatus = qr => qr.QrStatus;
        OrderByProvider<QrCodeInfo>.OrderByDic.Add(nameof(QrStatus),orderByStatus);
    }
}