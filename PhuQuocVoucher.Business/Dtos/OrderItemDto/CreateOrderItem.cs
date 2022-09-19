using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderItemDto;

public class CreateOrderItem : CreateDto, ICreateRequest<OrderItem>
{
    public int OrderId { get; set; }

    public int OrderProductId { get; set; }
    
    public int PriceId { get; set; }

    public int? ProfileId { get; set; }

    public DateTime? UseDate { get; set; }
}