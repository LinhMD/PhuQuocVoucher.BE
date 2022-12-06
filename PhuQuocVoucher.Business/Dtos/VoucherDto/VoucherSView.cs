using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class VoucherSView : BaseModel, IView<Voucher>, IDto
{
    public int Id { get; set; }
    
    public string VoucherName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public int? ServiceId { get; set; }

    public int ServiceTypeId { get; set; }
    
    public int? ProviderId { get; set; }

    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string?  SocialPost { get; set; }
    public int Inventory { get; set; }
    
    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public long SoldPrice { get; set; }
    
    public long VoucherValue { get; set; }
    
    public bool IsCombo { get; set; }


    public void InitMapper()
    {
        TypeAdapterConfig<Voucher, VoucherSView>.NewConfig().Map(
            view => view.Inventory,
            voucher => voucher.QrCodes.Count(qr => qr.QrCodeStatus == QrCodeStatus.Active));;
    }
}