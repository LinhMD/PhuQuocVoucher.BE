using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.PaymentDetailDto;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderDto;

public class OrderView : IView<Order>, IDto
{
    public DateTime? CreateAt { get; set; }
    
    public int Id { get; set; }

    public double TotalPrice { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Processing;

    public int? CustomerId { get; set; }

    public int? SellerId { get; set; }

    public string? SellerName { get; set; }

    public PaymentDetailView? PaymentDetail { get; set; }

    public void InitMapper() 
    {
        TypeAdapterConfig<Order, OrderView>.NewConfig()
            .Map(view => view.SellerName, order => order.Seller!.SellerName);
    }
}