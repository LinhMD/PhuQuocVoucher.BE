using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.QrCodeDto;

public class QrCodeSView : BaseModel, IView<QrCode>, IDto
{
    
    public int Id { get; set; }
    
    public string HashCode { get; set; }

    public int VoucherId { get; set; }
    
    public string? VoucherName { get; set; }
    
    public string? BannerImg { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public int? ProviderId { get; set; }
    
    public string? ProviderName { get; set; }

    public int ServiceId { get; set; }

    public string? UsePlace { get; set; }
    
    public QrCodeStatus QrCodeStatus { get; set; }
    
    public DateTime? UseDate { get; set; }
    
    public void InitMapper()
    {
        TypeAdapterConfig<QrCode, QrCodeSView>.NewConfig()
            .Map(view => view.VoucherName, code => code.Voucher.VoucherName)
            .Map(view => view.ProviderName, code => code.Provider!.ProviderName)
            .Map(view => view.BannerImg, code => code.Voucher.BannerImg)
            .Map(view => view.UsePlace, code => code.Service.UsePlace);
    }
}