
using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class VoucherView : IView<Voucher>, IDto
{
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

    public bool IsForKid { get; set; }

    
    public double? DisplayPrice { get; set; }
    
    public IEnumerable<PriceBookSView> Prices { get; set; }
    
    public int SlotNumber { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Voucher, VoucherView>.NewConfig();

    }
}