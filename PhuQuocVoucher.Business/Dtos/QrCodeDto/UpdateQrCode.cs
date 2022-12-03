using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.QrCodeDto;

public class UpdateQrCode : UpdateDto, IUpdateRequest<Voucher>
{
    public VoucherStatus Status { get; set; } = VoucherStatus.Active;
    
}