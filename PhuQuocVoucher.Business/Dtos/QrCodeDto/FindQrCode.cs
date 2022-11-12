using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.QrCodeDto;

public class FindQrCode : IFindRequest<QrCodeInfo>
{
    
    public int? Id { get; set; }
    
    public int? VoucherId { get; set; }

    public QRCodeStatus? Status { get; set; }
}