using System.Linq.Expressions;
using System.Text.Json.Serialization;
using CrudApiTemplate.OrderBy;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(HashCode), IsUnique = true)]
public class Voucher : BaseModel,  IOrderAble
{
    public int Id { get; set; }
    
    public string HashCode { get; set; }
    
    [JsonIgnore]
    public VoucherCompaign Compaign { get; set; }
    
    public string? ImgLink { get; set; }
    
    public int VoucherId { get; set; }

    public int? ProviderId { get; set; }

    public ServiceProvider? Provider { get; set; }

    public VoucherStatus QrStatus { get; set; } = VoucherStatus.Active;
    public void ConfigOrderBy()
    {
        SetUpOrderBy<Voucher>();
        Expression<Func<Voucher, VoucherStatus>> orderByStatus = qr => qr.QrStatus;
        OrderByProvider<Voucher>.OrderByDic.Add(nameof(QrStatus),orderByStatus);
    }
}