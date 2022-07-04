using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.CartItemDto;

public class FindCartItem : IFindRequest<CartItem>, IDto
{

    public int? Id { get; set; }

    public int? Quantity { get; set; }

    public int? ProductId { get; set; }

    public int? CartId { get; set; }
}