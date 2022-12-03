using System.Text.Json.Serialization;
using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.QrCodeDto;

public class QrCodeView : IView<Voucher>, IDto
{
    
    public int Id { get; set; }
    
    public string HashCode { get; set; }
    
    public int VoucherId { get; set; }

    public VoucherStatus QrStatus { get; set; }
    
}