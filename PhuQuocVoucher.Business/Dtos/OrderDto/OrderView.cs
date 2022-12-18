using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.PaymentDetailDto;
using PhuQuocVoucher.Business.Dtos.QrCodeDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderDto;

public class OrderView : BaseModel,IView<Order>, IDto
{
    public DateTime? CreateAt { get; set; }

    public int Id { get; set; }

    public double TotalPrice { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Processing;

    public int? CustomerId { get; set; }

    public int? SellerId { get; set; }
    
    public IList<QrCodeSimpleView> QrCodes { get; set; }

    public string? SellerName { get; set; }

    public PaymentDetailView? PaymentDetail { get; set; }

    public long CommissionFee { get; set; }

    public long SellerCommission { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Order, OrderView>.NewConfig()
            .Map(view => view.SellerName, order => order.Seller!.SellerName)
            .Map(view => view.CommissionFee, order => order.OrderItems.Select(item => item.CommissionFee).Sum())
            .Map(view => view.SellerCommission, order => order.OrderItems.Select(item => item.SellerCommission).Sum());
    }
}