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
        var cartView = await Repository.Find<CartView>(c => c.CustomerId == customerId).FirstOrDefaultAsync();
        if (cartView == null)
        {
            var cart = new Cart()
            {
                Customer = customer,
                Status = ModelStatus.Active,
                CreateAt = DateTime.Now,
                CustomerId = customerId,
                CartItems = new List<CartItem>()
            };
            await Repository.AddAsync(cart);
        }
        return cartView!;
    }

    public async Task<CartView> AddItemToCart(CreateCartItem item, int customerId)
    {
        var cart = await GetCartByCustomerAsync(customerId);
        var voucher = await UnitOfWork.Get<Voucher>().Find(c => c.Id == item.voucherId).FirstOrDefaultAsync();

        if (voucher == null)
        {
            throw new ModelNotFoundException($"Can not find voucher with id {item.voucherId}");
        }

        if (voucher.Inventory < item.Quantity)
            throw new ModelValueInvalidException("Cart item quantity exceed current inventory");

        var cartItem = new CartItem()
        {
            Voucher = voucher,
            Quantity = item.Quantity,
            CartId = cart.Id,
            CreateAt = DateTime.Now,
            Status = ModelStatus.Active,
            voucherId = item.voucherId,
            IsCombo = voucher.IsCombo
        };
        var itemView = (await UnitOfWork.Get<CartItem>().AddAsync(cartItem));
        return  await GetCartByCustomerAsync(customerId);
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

        var dict = updateCart.CartItems.Aggregate(new Dictionary<int, int>(), (dic, item) =>
        {
            dic.TryGetValue(item.voucherId, out var quantity);
            dic[item.voucherId] = item.Quantity + quantity;
            return dic;
        });

        var inventory = await UnitOfWork.Get<Voucher>()
            .Find(c => dict.Keys.Contains(c.Id) && c.EndDate >= DateTime.Now && c.Status == ModelStatus.Active && c.Inventory > 0)
            .ToDictionaryAsync(voucher => voucher.Id, voucher => voucher);

        var notFoundVoucher = dict.Keys.Except(inventory.Keys).ToList();
        if (notFoundVoucher.Any())
            throw new ModelNotFoundException($"vouchers not found : {notFoundVoucher.Aggregate("", (s, i) => s += ", " + i)}");
        
        if (dict.Keys.Any(id => (dict[id] > inventory[id].Inventory)))
        {
            throw new ModelValueInvalidException("Insufficient inventory");
        }
        
        foreach (var item in updateCart.CartItems)
        {
            var cartItem = new CartItem()
            {
                Voucher = inventory[item.voucherId],
                Quantity = item.Quantity,
                CartId = cart.Id,
                CreateAt = DateTime.Now,
                Status = ModelStatus.Active,
                voucherId = item.voucherId,
                IsCombo = inventory[item.voucherId].IsCombo
            };
            cartItem.CartId = cart.Id;
            await UnitOfWork.Get<CartItem>().AddAsync(cartItem);
            
        }

        return await GetCartByCustomerAsync(customerId);
    }

    public async Task<List<RemainVoucherInventory>> CheckCart(int customerId)
    {
        var cart = await Repository.Find(cart => cart.CustomerId == customerId).FirstOrDefaultAsync();
        if (cart == null)
            throw new ModelNotFoundException("Cart of customer not found");
        var dict = cart.CartItems.Aggregate(new Dictionary<int, int>(), (dic, item) =>
        {
            dic[item.voucherId] += item.Quantity;
            return dic;
        });

        var inventory = await UnitOfWork.Get<Voucher>()
            .Find(c => dict.Keys.Contains(c.Id) && !c.IsCombo)
            .Select(c => new {c.Id, c.Inventory}).Select(value => new RemainVoucherInventory
            {
                RemainInventory = value.Inventory,
                VoucherId = value.Id
            }).ToListAsync();
        
        inventory.AddRange(await UnitOfWork.Get<Voucher>()
            .Find(c => dict.Keys.Contains(c.Id) && c.IsCombo)
            .Select(c => new {c.Id, Inventory= c.Vouchers.Select(v => v.Voucher.Inventory).Min()})
            .Select(value => new RemainVoucherInventory
            {
                RemainInventory = value.Inventory,
                VoucherId = value.Id
            }).ToListAsync());
        return inventory;
    }


    public async Task<List<RemainVoucherInventory>> CheckItem(UpdateCart cart)
    {
        var dict = cart.CartItems.Aggregate(new Dictionary<int, int>(), (dic, item) =>
        {
            dic.TryGetValue(item.voucherId, out int value);
            dic[item.voucherId] = item.Quantity + value;
            return dic;
        });

        var inventory = await UnitOfWork.Get<Voucher>()
            .Find(c => dict.Keys.Contains(c.Id) && !c.IsCombo)
            .Select(c => new {c.Id, c.Inventory}).Select(value => new RemainVoucherInventory
            {
                RemainInventory = value.Inventory,
                VoucherId = value.Id
            }).ToListAsync();
        
        inventory.AddRange(await UnitOfWork.Get<Voucher>()
            .Find(c => dict.Keys.Contains(c.Id) && c.IsCombo)
            .Select(c => new {c.Id, InventoryMin = c.Vouchers.Select(v => v.Voucher.Inventory).Min(), c.Inventory})
            .Select(value => new RemainVoucherInventory
            {
                RemainInventory = value.Inventory > value.InventoryMin ? value.InventoryMin : value.Inventory,
                VoucherId = value.Id
            }).ToListAsync());
        var notFoundVoucher = dict.Keys.Except(inventory.Select(i => i.VoucherId)).ToList();
        /*
        if (notFoundVoucher.Any())
            throw new ModelNotFoundException($"vouchers not found : {notFoundVoucher.Aggregate("", (s, i) => s += ", " + i)}");
        
        if (dict.Keys.Any(id => (dict[id] > inventory[id].RemainInventory))) 
        {
            throw new ModelValueInvalidException("Insufficient inventory");
        }*/
        return inventory;
    }
}