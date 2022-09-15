using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.ProductDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CartItemDto;

public class CartItemView : IView<CartItem>, IDto
{
    public int Id { get; set; }

    public int Quantity { get; set; }
    
    public ProductView Product { get; set; }
    public double Price { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<CartItem, CartItemView>.NewConfig()
            .Map(view => view.Price, item => item.Price!.Price);
    }
}