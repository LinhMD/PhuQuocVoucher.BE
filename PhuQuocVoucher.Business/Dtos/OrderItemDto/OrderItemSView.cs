using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderItemDto;

public class OrderItemSView : IView<OrderItem>, IDto
{
    public int Id { get; set;}

    public int OrderId { get; set; }

    public int OrderProductId { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<OrderItem, OrderItemSView>.NewConfig();
    }
}