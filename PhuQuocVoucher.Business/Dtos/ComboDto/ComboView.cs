using CrudApiTemplate.View;
using Mapster;
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

    public double Price { get; set; }

    public ProductView? Product { get; set; }

    public int? ProductId { get; set; }

    public IEnumerable<VoucherSView> Vouchers { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Combo, ComboView>.NewConfig()
            .Map(view => view.Vouchers, combo => combo.Vouchers);
    }
}