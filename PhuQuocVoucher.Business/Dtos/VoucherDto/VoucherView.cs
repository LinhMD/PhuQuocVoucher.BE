using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.ProductDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class VoucherView : IView<Voucher>, IDto
{
    public int Id { get; set; }

    public string VoucherName { get; set; }

    public double Price { get; set; }

    public int Inventory { get; set; }

    public int? LimitPerDay { get; set; }

    public bool? IsRequireProfileInfo { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public ProductView? Product { get; set; }

    public int? ProductId { get; set; }

    public int ServiceId { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Voucher, VoucherView>.NewConfig();

    }
}