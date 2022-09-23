using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.CartDto;
using PhuQuocVoucher.Business.Dtos.CartItemDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface ICartService : IServiceCrud<Cart>
{
    public Task<CartView> GetCartByCustomerAsync(int customerId);

    public Task<CartView> AddItemToCart(CreateCartItem item, int customerId);
}