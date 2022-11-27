
using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Business.Dtos.ServiceDto;
using PhuQuocVoucher.Business.Dtos.ServiceTypeDto;
using PhuQuocVoucher.Business.Dtos.TagDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class VoucherView : IView<Voucher>, IDto
{
    public DateTime? CreateAt{ get; set; }

    public DateTime? UpdateAt { get; set; }
    public int Id { get; set; }

    public string VoucherName { get; set; }

    public int Inventory { get; set; }

    public int? LimitPerDay { get; set; }

    public bool? IsRequireProfileInfo { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int ServiceId { get; set; }
    
    
    
    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public ServiceSView Service { get; set; }
    
    public ServiceTypeView ServiceType { get; set; }
    
    
    public IEnumerable<PriceBookSView> Prices { get; set; }
    
    public int SlotNumber { get; set; }
    
    public IEnumerable<TagView> Tags { get; set; }

    public ModelStatus Status { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Voucher, VoucherView>.NewConfig()
            .Map(view => view.Tags, voucher => voucher.Tags.Select(tag => tag.Tag))
            .Map(view => view.ServiceType , v => v.Service.ServiceType)
            .Map(view => view.Inventory, voucher => voucher.QrCodeInfos.Count(qr => qr.Status== QRCodeStatus.Active));

    }
}