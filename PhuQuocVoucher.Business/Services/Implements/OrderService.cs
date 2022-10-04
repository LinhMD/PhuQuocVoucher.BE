﻿using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Api.Ultility;
using PhuQuocVoucher.Business.Dtos.CartDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Dtos.OrderItemDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class OrderService : ServiceCrud<Order>, IOrderService
{
    private ILogger<OrderService> _logger;
    public OrderService(IUnitOfWork work, ILogger<OrderService> logger) : base(work.Get<Order>(), work, logger)
    {
        _logger = logger;
    }

    public async Task<(IList<OrderView>, int)> GetOrdersByCustomerId(int cusId, PagingRequest request, OrderRequest<Order> sortBy)
    {
        var filter = Repository.Find(order => order.CustomerId == cusId);

        var total = filter.Count();

        var result = await filter
            .OrderBy(sortBy)
            .ProjectToType<OrderView>()
            .Paging(request).ToListAsync();

        return (result, total);
    }

   

    private async Task ValidateInventoryEnoughAsync(IEnumerable<OrderItem> orderItems)
    {
        var itemCount = orderItems.GroupBy(o => o.OrderProductId);
        //check inventory
        foreach (var items in itemCount)
        {
            var voucher = await UnitOfWork.Get<Voucher>()
                .Find(voucher => voucher.ProductId == items.Key && voucher.Inventory >= items.Count()).FirstOrDefaultAsync();
            
            if (voucher == null)
            {
                throw new ModelValueInvalidException($"Voucher with product id {items.ToList().First().OrderProductId} do not have enough inventory");
            }
            voucher.Inventory -= items.Count();
        }
    }

    public async Task<OrderView> PlaceOrderAsync(CartView cart, int cusId, int? sellerId = null)
    {
        //create order for the id
        var order = new Order()
        {
            CustomerId = cusId,
            TotalPrice = 0D,
            SellerId = sellerId
        };
        await UnitOfWork.Get<Order>().AddAsync(order);

        double total = 0;
        //create order item
        var orderItems = new List<OrderItem>();
        foreach (var items in cart!.CartItems)
        {
            for (var i = 0; i < items.Quantity; i++)
            {
                var orderItem = new OrderItem
                {
                    PriceId = items.PriceId,
                    OrderProductId = items.ProductId,
                    OrderId = order.Id
                };
                orderItem.Validate();
                orderItems.Add(orderItem);
                total += items.Price;
            }
        }
        order.TotalPrice = total;
        await ValidateInventoryEnoughAsync(orderItems);
        await UnitOfWork.Get<OrderItem>().AddAllAsync(orderItems);
        await UnitOfWork.Get<CartItem>().RemoveAllAsync(cart.CartItems.Select(c => new CartItem{Id = c.Id}));
        order.OrderItems = orderItems;
        
        order.Validate();
            
        return order.Adapt<OrderView>();
    }
    
    public async Task<OrderView> CreateOrderAsync(CreateOrder createOrder)
    {
        try
        {
            var orderItems = createOrder.OrderItems
                .Select(o => (o as ICreateRequest<OrderItem>).CreateNew(UnitOfWork)).ToList();
            
            orderItems.ForEach(o => o.Validate());
            
            await ValidateInventoryEnoughAsync(orderItems);

            await UnitOfWork.Get<OrderItem>().AddAllAsync(orderItems);

            var order = (createOrder as ICreateRequest<Order>).CreateNew(UnitOfWork);
            order.TotalPrice = orderItems.Select(item => item.Price.Price).Sum();
            
            order.OrderItems = orderItems;
            order.Validate();
            
            return (await UnitOfWork.Get<Order>().AddAsync(order)).Adapt<OrderView>();
        }
        catch (DbUpdateException e)
        {
            e.InnerException?.StackTrace.Dump();
            throw new DbQueryException(e.InnerException?.Message!, DbError.Create);
        }
    }
}