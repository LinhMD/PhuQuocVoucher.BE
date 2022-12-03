using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class VoucherSView : IView<VoucherCompaign>, IDto
{
    public int Id { get; set; }

    public string VoucherName { get; set; }

    public double Price { get; set; }

    public int Inventory { get; set; }

    public int? LimitPerDay { get; set; }

    public bool? IsRequireProfileInfo { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    

    public int ServiceId { get; set; }
    
    public int SlotNumber { get; set; }
    
    public string? Summary { get; set; }
    
    public string? BannerImg { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<VoucherCompaign, VoucherSView>.NewConfig();

    }
}