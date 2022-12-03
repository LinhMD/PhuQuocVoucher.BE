using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.QrCodeDto;

public class CreateQrCode: CreateDto, ICreateRequest<Voucher>
{
    public Guid HashCode { get; set; }
        
    public int VoucherId { get; set; }

    public VoucherStatus Status { get; set; } = VoucherStatus.Active;
    
}