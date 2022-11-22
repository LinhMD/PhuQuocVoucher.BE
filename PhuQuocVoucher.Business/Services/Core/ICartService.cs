using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.CartDto;
using PhuQuocVoucher.Business.Dtos.CartItemDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface ICartService : IServiceCrud<Cart>
{
    public Task<CartView> GetCartByCustomerAsync(int customerId);

    public Task<CartView> AddItemToCart(CreateCartItem item, int customerId);

    public Task<CartView> UpdateCartItems(IList<UpdateCartItem> updateCartItem, int cartId, int customerId);

    public Task ClearCart(int cartId);

    public Task<CartView> UpdateCartAsync(UpdateCart updateCart, int customerId);

    public  Task<List<RemainVoucherInventory>> CheckCart(int customerId);
}