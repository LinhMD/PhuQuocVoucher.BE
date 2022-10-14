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
            var product = await UnitOfWork.Get<Product>()
                .Find(product =>  product.Id == items.Key && product.Inventory >= items.Count()).FirstOrDefaultAsync();
            
            if (product == null)
            {
                throw new ModelValueInvalidException($"Voucher with product id {items.ToList().First().OrderProductId} do not have enough inventory");
            }
            product.Inventory -= items.Count();
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
            //create order for the id
            var order = new Order()
            {
                CustomerId = createOrder.CustomerId,
                TotalPrice = 0D,
                SellerId = createOrder.SellerId
            };
            order.Validate();
            await UnitOfWork.Get<Order>().AddAsync(order);
            foreach (var item in createOrder.OrderItems)
            {
                item.OrderId = order.Id;
            }
            var orderItems = createOrder.OrderItems
                .Select(o => (o as ICreateRequest<OrderItem>).CreateNew(UnitOfWork)).ToList();
            
            foreach (var item in orderItems)
            {
                try
                {
                    item.Validate();
                }
                catch (Exception e)
                {
                    e.StackTrace.Dump();
                }
            }
            
            await ValidateInventoryEnoughAsync(orderItems);

            await UnitOfWork.Get<OrderItem>().AddAllAsync(orderItems);

            order.TotalPrice = await UnitOfWork.Get<OrderItem>().Find(item => item.OrderId == order.Id).Select(item => item.Price.Price).SumAsync();
            
            order.OrderItems = orderItems;

            await UnitOfWork.CompleteAsync();
            return order.Adapt<OrderView>();
        }
        catch (DbUpdateException e)
        {
            e.InnerException?.StackTrace.Dump();
            throw new DbQueryException(e.InnerException?.Message!, DbError.Create);
        }
    }
}