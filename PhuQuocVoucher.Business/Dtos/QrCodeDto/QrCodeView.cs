using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.ProviderDto;
using PhuQuocVoucher.Business.Dtos.ServiceDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.QrCodeDto;

public class QrCodeView : BaseModel, IView<QrCode>, IDto
{
    public int Id { get; set; }
    
    public string HashCode { get; set; }
    
    public int VoucherId { get; set; }
    
    public VoucherSView Voucher { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public int? ProviderId { get; set; }
    
    public SimpleProviderView? Provider { get; set; }

    public ServiceSView Service { get; set; }

    public int ServiceId { get; set; }

    public QrCodeStatus QrCodeStatus { get; set; }
    
    public string? UsePlace { get; set; }
    
    public DateTime? UseDate { get; set; }
    
    public void InitMapper()
    {
        TypeAdapterConfig<QrCode, QrCodeView>.NewConfig()
            .Map(view => view.UsePlace, code => code.Service.UsePlace);
    }
}