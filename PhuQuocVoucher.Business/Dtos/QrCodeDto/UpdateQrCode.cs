using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.QrCodeDto;

public class UpdateQrCode : UpdateDto, IUpdateRequest<QrCodeInfo>
{
    public QRCodeStatus Status { get; set; } = QRCodeStatus.Active;
    
}