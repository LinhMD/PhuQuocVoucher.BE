using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.CustomerDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Dtos.UserDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.SellerDto;

public class SellerView : IView<Seller>, IDto
{
    public int? Id { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? SellerName { get; set; }

    public int? UserInfoId { get; set; }

    public UserView? UserInfo { get; set; }

    public float? CommissionRate { get; set; }

    public double? Profit { get; set; }

    public IEnumerable<OrderView> Orders { get; set; }


    public ModelStatus? Status { get; set; }
    public void InitMapper()
    {
        TypeAdapterConfig<Seller, SellerView>.NewConfig().Map(
            sv => sv.Profit,
            s => s.HandleOrders.Select(o => o.TotalPrice).Sum());
    }
}