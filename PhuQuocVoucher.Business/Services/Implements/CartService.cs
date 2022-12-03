using System.Diagnostics;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Google.Apis.Json;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.CartDto;
using PhuQuocVoucher.Business.Dtos.CartItemDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class CartService : ServiceCrud<Cart>, ICartService
{
    private ILogger<CartService> _logger;
    public CartService(IUnitOfWork work, ILogger<CartService> logger) : base(work.Get<Cart>(), work, logger)
    {
        _logger = logger;
    }

    public async Task<CartView> GetCartByCustomerAsync(int customerId)
    {
        
        var customer = await UnitOfWork.Get<Customer>().GetAsync(customerId);
        if (customer == null) throw new ModelNotFoundException($"Customer {customerId} not found!!!");
        
        return await Repository.Find<CartView>(c => c.CustomerId == customerId).FirstOrDefaultAsync() ?? 
               (await Repository.AddAsync(new Cart
        {
            Status = ModelStatus.Active,
            CreateAt = DateTime.Now,
            CustomerId = customerId
        })).Adapt<CartView>();
    }

    public async Task<CartView> AddItemToCart(CreateCartItem item, int customerId)
    {
        var cart = await GetCartByCustomerAsync(customerId);
        var cartItem = item.Adapt<CartItem>();
        
        cartItem.CartId = cart.Id;
        var priceBook = await UnitOfWork.Get<PriceBook>().GetAsync(item.PriceId);
        
        if (priceBook == null)
        {
            throw new ModelNotFoundException($"Price Id {item.PriceId} not found!!");
        } 
        
        cartItem.VoucherId = priceBook.VoucherId;
        
        var itemView = (await UnitOfWork.Get<CartItem>().AddAsync(cartItem)).Adapt<CartItemView>();
        cart.CartItems.Add(itemView);
        return cart;
    }

    public async Task<CartView> UpdateCartItems(IList<UpdateCartItem> updateCartItem, int cartId, int customerId)
    {
        var itemFound = await UnitOfWork.Get<CartItem>()
            .Find(c =>
                c.CartId == cartId
                && updateCartItem.Select(ci => ci.CartItemId).Contains(c.Id))
            .ToListAsync();
       
        if (!itemFound.Any()) throw new ModelNotFoundException($"Cart Items not found!!!");
       
        foreach (var cartItem in itemFound)
        {
            foreach (var updateItem in updateCartItem)
            {
                if (cartItem.Id == updateItem.CartItemId)
                {
                    cartItem.Quantity = updateItem.Quantity;
                }
            }
        }

        await UnitOfWork.CompleteAsync();
        return await GetCartByCustomerAsync(customerId);
    }

    public async Task ClearCart(int cartId)
    {
        var cartItems = await UnitOfWork.Get<CartItem>().Find(i => i.CartId == cartId).ToListAsync();
        await UnitOfWork.Get<CartItem>().RemoveAllAsync(cartItems);
    }

    public async Task<CartView> UpdateCartAsync(UpdateCart updateCart, int customerId)
    {
        var cart = await GetCartByCustomerAsync(customerId);
        await ClearCart(cart.Id);
        cart.CartItems.Clear();
        foreach (var item in updateCart.CartItems)
        {
            var cartItem = item.Adapt<CartItem>();
        
            cartItem.CartId = cart.Id;
            var priceBook = await UnitOfWork.Get<PriceBook>().GetAsync(item.PriceId);
        
            if (priceBook == null)
            {
                throw new ModelNotFoundException($"Price Id {item.PriceId} not found!!");
            }
            cartItem.VoucherId = priceBook.VoucherId;
            var itemView = (await UnitOfWork.Get<CartItem>().AddAsync(cartItem)).Adapt<CartItemView>();
            cart.CartItems.Add(itemView);
        }
        
        return cart;
    }

    public async Task<List<RemainVoucherInventory>> CheckCart(int customerId)
    {
        var cart = await Repository.Find(cart => cart.CustomerId == customerId).FirstOrDefaultAsync();
        if (cart == null)
            throw new ModelNotFoundException("Cart of customer not found");
        var items =  cart.CartItems
            .Select(item => (item.VoucherId, item.UseDate, item.VoucherCompaign.LimitPerDay))
            .Where(i => i.UseDate != null)
            .Distinct()
            .Where(item => item.LimitPerDay != null)
            .Select( cartItem =>  new RemainVoucherInventory() {
                LimitPerDay = (cartItem.LimitPerDay ?? 0),
                AlreadyOrder = ( UnitOfWork.Get<OrderItem>()
                    .Find(item => 
                        cartItem.UseDate != null
                        && item.VoucherId == cartItem.VoucherId 
                        && item.UseDate != null 
                        && item.UseDate.Value.Date.Date == cartItem.UseDate.Value.Date.Date)
                    .Count()), 
                RemainInventory = UnitOfWork.Get<Voucher>().Find(info => info.VoucherId == cartItem.VoucherId)
                    .Count(),
                VoucherId = cartItem.VoucherId, 
                Date = cartItem.UseDate}).ToList();

        items.AddRange(cart.CartItems
            .Select(item => (item.VoucherId, item.UseDate, item.VoucherCompaign.LimitPerDay))
            .Where(i => i.UseDate != null)
            .Distinct()
            .Where(item => item.LimitPerDay == null)
            .Select( cartItem => new RemainVoucherInventory
            {
                RemainInventory = UnitOfWork.Get<Voucher>().Find(info => info.VoucherId == cartItem.VoucherId)
                    .Count(),
                Date = cartItem.UseDate,
                VoucherId = cartItem.VoucherId
            }));
        return items;
    }
    
    
    public async Task<List<RemainVoucherInventory>> CheckItem(UpdateCart cart)
    {
        var priceIds = cart.CartItems.Select(c => c.PriceId).ToList();

        var priceBooks = await UnitOfWork.Get<PriceBook>()
            .Find(p => priceIds.Contains(p.Id))
            .Select(p => new{p.Id, p.VoucherId})
            .ToDictionaryAsync(price => price.Id);
        
        var voucherIds = priceBooks.Values.Select(book => book.VoucherId).Distinct();
        
        var limitPerDays = await UnitOfWork.Get<VoucherCompaign>()
            .Find(v => voucherIds.Contains(v.Id))
            .Select(v => new{v.Id,v.LimitPerDay})
            .ToDictionaryAsync(v => v.Id);
        
        var cartItems = cart.CartItems.Select(i => new
        {
            priceBooks[i.PriceId].VoucherId,
            i.UseDate,
            limitPerDays[priceBooks[i.PriceId].VoucherId].LimitPerDay
        }).ToList();
        
        var items =  cartItems
            .Select(item => (item.VoucherId, item.UseDate, item.LimitPerDay))
            .Where(i => i.UseDate != null)
            .Distinct()
            .Where(item => item.LimitPerDay != null)
            .Select( cartItem =>  new RemainVoucherInventory() {
                LimitPerDay = (cartItem.LimitPerDay ?? 0),
                AlreadyOrder = ( UnitOfWork.Get<OrderItem>()
                    .Find(item => 
                        cartItem.UseDate != null
                        && item.VoucherId == cartItem.VoucherId 
                        && item.UseDate != null 
                        && item.UseDate.Value.Date.Date == cartItem.UseDate.Value.Date.Date)
                    .Count()), 
                RemainInventory = UnitOfWork.Get<Voucher>().Find(info => info.VoucherId == cartItem.VoucherId)
                    .Count(),
                VoucherId = cartItem.VoucherId, 
                Date = cartItem.UseDate
            }).ToList();

        items.AddRange(cartItems
            .Select(item => (item.VoucherId, item.UseDate, item.LimitPerDay))
            .Where(i => i.UseDate != null)
            .Distinct()
            .Where(item => item.LimitPerDay == null)
            .Select( cartItem => new RemainVoucherInventory
            {
                RemainInventory = UnitOfWork.Get<Voucher>().Find(info => info.VoucherId == cartItem.VoucherId)
                    .Count(),
                Date = cartItem.UseDate,
                VoucherId = cartItem.VoucherId
            }));
        return items;
    }
}