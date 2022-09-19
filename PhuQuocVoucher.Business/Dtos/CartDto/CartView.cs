using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.CartItemDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CartDto;

public class CartView : IView<Cart>, IDto
{
    
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public IList<CartItemView> CartItems { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Cart, CartView>.NewConfig();
    }
}