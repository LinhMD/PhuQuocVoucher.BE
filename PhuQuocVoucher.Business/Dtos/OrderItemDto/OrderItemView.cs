using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderItemDto;

public class OrderItemView : IView<OrderItem>, IDto
{
    public int Id { get; set;}

    public int OrderId { get; set; }

    public int OrderProductId { get; set; }
    
    public int PriceId { get; set; }
    
    public int? ProfileId { get; set; }
    
    public DateTime? UseDate { get; set; }
    
    public PriceBookView Price { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<OrderItem, OrderItemView>.NewConfig();
    }
}