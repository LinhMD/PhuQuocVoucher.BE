using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.QrCodeDto;

public class UpdateVoucher : UpdateDto, IUpdateRequest<QrCode>
{
    public QrCodeStatus Status { get; set; } = QrCodeStatus.Active;
    
}