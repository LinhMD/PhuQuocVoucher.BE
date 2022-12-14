using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.ProviderDto;
using PhuQuocVoucher.Business.Dtos.ServiceDto;
using PhuQuocVoucher.Business.Dtos.ServiceTypeDto;
using PhuQuocVoucher.Business.Dtos.TagDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ComboDto;

public class ComboView : BaseModel, IView<Voucher>, IDto
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
    
    public int Inventory { get; set; }
    
    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public string?  SocialPost { get; set; }
    public long SoldPrice { get; set; }
    
    public bool IsCombo { get; set; }
    public IEnumerable<TagView> Tags { get; set; }

    public IList<Review> Reviews { get; set; }
    public IEnumerable<VoucherSView> Vouchers { get; set; }

    public VoucherKPI? Kpi { get; set; }

    public ModelStatus Status { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Voucher, ComboView>.NewConfig()
            .Map(view => view.Tags, voucher => voucher.Tags.Select(tag => tag.Tag))
            .Map(view => view.Vouchers, combo => combo.Vouchers.Select(cv => cv.Voucher));
    }
}