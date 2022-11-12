using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.QrCodeDto;

public class CreateQrCode: CreateDto, ICreateRequest<QrCodeInfo>
{
    public Guid HashCode { get; set; }
        
    public int VoucherId { get; set; }

    public QRCodeStatus Status { get; set; } = QRCodeStatus.Active;
    
}