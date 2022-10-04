
using System.Text.Json.Serialization;
using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Business.Dtos.ProductDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ComboDto;

public class ComboView : IView<Combo>, IDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }


    [JsonIgnore]
    public ProductView? Product { get; set; }
    
    
    public double Price { get; set; }
    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public bool IsForKid { get; set; }

    public ProductType Type { get; set; }
    
    public IEnumerable<PriceBookSView> Prices { get; set; }

    public int? ProductId { get; set; }

    public IEnumerable<VoucherSView> Vouchers { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Combo, ComboView>.NewConfig()
            .Map(view => view.Vouchers, combo => combo.Vouchers)
            .Map(view => view.Description, combo => combo.Product!.Description)
            .Map(view => view.Summary, combo => combo.Product!.Summary)
            .Map(view => view.BannerImg, combo => combo.Product!.BannerImg)
            .Map(view => view.Content, combo => combo.Product!.Content)
            .Map(view => view.Type, combo => combo.Product!.Type)
            .Map(view => view.Price, combo => combo.Product!.Prices.FirstOrDefault(p => p.IsDefault)!.Price);
    }
}