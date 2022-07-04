using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.CartItemDto;

public class UpdateCartItem : UpdateDto, IUpdateRequest<CartItem>
{
    public int Quantity { get; set; }
}