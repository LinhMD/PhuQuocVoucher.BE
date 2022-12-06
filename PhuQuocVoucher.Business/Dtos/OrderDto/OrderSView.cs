using CrudApiTemplate.View;
using PhuQuocVoucher.Business.Dtos.CustomerDto;
using PhuQuocVoucher.Business.Dtos.PaymentDetailDto;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderDto;

public class OrderSView : BaseModel,IView<Order>, IDto
{

    public int Id { get; set; }

    public double TotalPrice { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Processing;

    public int? CustomerId { get; set; }

    public CustomerSView Customer { get; set; }

    public int? SellerId { get; set; }

    public SellerSView? Seller { get; set; }

    public PaymentDetailView? PaymentDetail { get; set; }
    
}