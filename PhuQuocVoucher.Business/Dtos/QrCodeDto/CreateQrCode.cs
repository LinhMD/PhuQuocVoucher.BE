using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.QrCodeDto;

public class CreateQrCode : CreateDto, ICreateRequest<QrCode>
{
    public Guid HashCode { get; set; }

    public string? ImgLink { get; set; }
    
    public int VoucherId { get; set; }
    
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public int? ProviderId { get; set; }
    
    public int ServiceId { get; set; }
    
    public QrCodeStatus QrCodeStatus { get; set; } = QrCodeStatus.Active;
}