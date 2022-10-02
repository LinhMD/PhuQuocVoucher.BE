using CrudApiTemplate.View;
using PhuQuocVoucher.Business.Dtos.OrderItemDto;
using PhuQuocVoucher.Business.Dtos.PaymentDetailDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderDto;

public class OrderSView : IView<Order>, IDto
{
    public int Id { get; set; }

    public double TotalPrice { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Processing;

    public int? CustomerId { get; set; }

    public int? SellerId { get; set; }

    public PaymentDetailView? PaymentDetail { get; set; }

    public IEnumerable<OrderItemSView> OrderItems { get; set; }
}