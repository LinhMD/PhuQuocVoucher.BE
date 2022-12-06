using CrudApiTemplate.View;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.QrCodeDto;

public class QrCodeSView : BaseModel, IView<QrCode>, IDto
{
    
    public int Id { get; set; }
    
    public string HashCode { get; set; }

    public int VoucherId { get; set; }
    
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public int? ProviderId { get; set; }

    public int ServiceId { get; set; }

    public QrCodeStatus QrCodeStatus { get; set; }
    
    public DateTime? UseDate { get; set; }
}