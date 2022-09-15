using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CartItemDto;

public class CreateCartItem : CreateDto, ICreateRequest<CartItem>
{
    public int Quantity { get; set; }

    public int ProductId { get; set; }
    
    public int PriceId { get; set; }
}