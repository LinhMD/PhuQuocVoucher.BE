using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.SellerDto;

public class SellerSView : BaseModel,IView<Seller>, IDto
{
    public int? Id { get; set; }

    public string? SellerName { get; set; }

    public int? UserInfoId { get; set; }

    public float? CommissionRate { get; set; }

    public double? Profit { get; set; }
    
    public int Exp { get; set; }
    public string Rank { get; set; }
    
    public void InitMapper()
    {
        TypeAdapterConfig<Seller, SellerSView>.NewConfig().Map(
            sv => sv.Profit,
            s => s.HandleOrders.Select(o => o.TotalPrice).Sum())
            .Map(
                sv => sv.Rank,
                s => s.Rank.Rank);
    }
}