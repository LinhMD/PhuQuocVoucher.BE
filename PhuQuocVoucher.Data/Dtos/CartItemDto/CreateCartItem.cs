using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.CartItemDto;

public class CreateCartItem : CreateDto, ICreateRequest<CartItem>
{
    public int Quantity { get; set; }

    public int ProductId { get; set; }

    public int CartId { get; set; }
}