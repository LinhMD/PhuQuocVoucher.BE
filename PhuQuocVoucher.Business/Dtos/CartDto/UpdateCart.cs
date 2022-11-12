using CrudApiTemplate.Request;
using PhuQuocVoucher.Business.Dtos.CartItemDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CartDto;

public class UpdateCart : UpdateDto, IUpdateRequest<Cart>
{
    public IList<CreateCartItem> CartItems { get; set; }
}