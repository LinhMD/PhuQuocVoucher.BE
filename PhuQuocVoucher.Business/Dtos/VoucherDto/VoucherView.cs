using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.ProviderDto;
using PhuQuocVoucher.Business.Dtos.ServiceDto;
using PhuQuocVoucher.Business.Dtos.ServiceTypeDto;
using PhuQuocVoucher.Business.Dtos.TagDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class VoucherView : BaseModel, IView<Voucher>, IDto
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

    public int SoldNumber { get; set; }

    public long VoucherValue { get; set; }
    public long Revenue { get; set; }
    
    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public long SoldPrice { get; set; }
    
    public bool IsCombo { get; set; }

    public ServiceSView? Service { get; set; }

    public ServiceTypeView? ServiceType { get; set; }

    public ProviderView? Provider { get; set; }

    public IEnumerable<TagView> Tags { get; set; }

    public IList<Review> Reviews { get; set; }

    public ModelStatus Status { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Voucher, VoucherView>.NewConfig()
            .Map(view => view.Tags, voucher => voucher.Tags.Select(tag => tag.Tag))
            .Map(view => view.ServiceType, v => v.Service == null ? null : v.Service.ServiceType)
            .Map(view => view.Inventory,
                voucher => voucher.QrCodes.Count(qr => qr.QrCodeStatus == QrCodeStatus.Active))
            .Map(view => view.SoldNumber, voucher => voucher.QrCodes.Count(qr => qr.QrCodeStatus == QrCodeStatus.Commit || qr.QrCodeStatus == QrCodeStatus.Used))
            .Map(view => view.Revenue, voucher => voucher.QrCodes.Select(q => q.SoldPrice).Sum());
    }
}