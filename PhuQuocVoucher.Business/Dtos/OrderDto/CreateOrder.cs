using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderDto;

public class CreateOrder : CreateDto, ICreateRequest<Order>
{

    public DateTime CreateDate { get; set; }
    public OrderStatus OrderStatus => OrderStatus.Processing;

    public int? CustomerId { get; set; }
    
    public int? SellerId { get; set; }
}