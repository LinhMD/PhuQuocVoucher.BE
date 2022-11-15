using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderItemDto;

public class UpdateOrderItem : UpdateDto, IUpdateRequest<OrderItem>
{
    public int? ProfileId { get; set; }
}