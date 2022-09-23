using CrudApiTemplate.Request;
using PhuQuocVoucher.Business.Dtos.OrderItemDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderDto;

public class CreateOrder : CreateDto, ICreateRequest<Order>
{
    public int Id { get; set; }

    public DateTime CreateDate { get; set; }
    public OrderStatus OrderStatus => OrderStatus.Processing;

    public int? CustomerId { get; set; }
    
    public int? SellerId { get; set; }
    public IList<CreateOrderItem> OrderItems { get; set; }
}