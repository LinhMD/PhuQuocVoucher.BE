using System.Text.Json.Serialization;
using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Business.Dtos.ProductDto;
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
    
    [JsonIgnore]
    public ProductView? Product { get; set; }

    public int? ProductId { get; set; }

    public int ServiceId { get; set; }
    
    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public bool IsForKid { get; set; }

    public double Price { get; set; }
    public ProductType Type { get; set; }
    
    public IEnumerable<PriceBookSView> Prices { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Voucher, VoucherView>.NewConfig()
            .Map(view => view.Description, voucher => voucher.Product!.Description)
            .Map(view => view.Summary, voucher => voucher.Product!.Summary)
            .Map(view => view.BannerImg, voucher => voucher.Product!.BannerImg)
            .Map(view => view.Content, voucher => voucher.Product!.Content)
            .Map(view => view.Type, voucher => voucher.Product!.Type)
            .Map(view => view.Prices, voucher => voucher.Product!.Prices)
            .Map(view => view.Price, voucher => voucher.Product!.Prices.FirstOrDefault(p => p.IsDefault)!.Price);

    }
}