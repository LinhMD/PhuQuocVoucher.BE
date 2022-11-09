using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(HashCode), IsUnique = true)]
public class QrCodeInfo
{
    public int Id { get; set; }
    
    [JsonIgnore]
    public string HashCode { get; set; }
    
    [JsonIgnore]
    public Voucher Voucher { get; set; }
    
    public int VoucherId { get; set; }

    public QRCodeStatus Status { get; set; } = QRCodeStatus.Active;

}