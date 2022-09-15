using System.Security.Claims;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.CustomBinding;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Business.Dtos.CartDto;
using PhuQuocVoucher.Business.Dtos.CartItemDto;
using PhuQuocVoucher.Business.Dtos.CustomerDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Repositories;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]s")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    private readonly ICartService _cartService;

    private readonly IOrderService _orderService;

    private readonly IUnitOfWork _work;

    private readonly ILogger<CustomerController> _logger;

    private readonly IRepository<Customer> _repo;

    public CustomerController(ILogger<CustomerController> logger, IUnitOfWork work,
        ICustomerService customerService, IOrderService orderService, ICartService cartService)
    {
        _customerService = customerService;
        _logger = logger;
        _work = work;
        _orderService = orderService;
        _cartService = cartService;
        _repo = work.Get<Customer>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]FindCustomer request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        
        return Ok((await _customerService.GetAsync<CustomerSView>(new GetRequest<Customer>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Customer>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateCustomer request)
    {
        return Ok(await _customerService.CreateCustomerAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find<CustomerSView>(cus => cus.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Customer)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateCustomer request, int id)
    {
        return Ok(await _customerService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _customerService.DeleteAsync(id));
    }

    [HttpGet("{id:int}/cart")]
    [Authorize]
    public async Task<IActionResult> GetCart(int id)
    {
        var cart = await _work.Get<Cart>().Find(cus => cus.CustomerId == id).FirstOrDefaultAsync() ??
                   await _cartService.CreateAsync(new CreateCart
        {
            CustomerId = id
        });
        return Ok(cart);
    }
    
    [HttpPost("/cart/items")]
    [Authorize]
    public async Task<IActionResult> AddCartItem(CreateCartItem item)
    {
        var cartId = int.Parse(User.FindFirstValue("CartId"));

        var cartItem = item.Adapt<CartItem>();
        cartItem.CartId = cartId;

        await _work.Get<CartItem>().AddAsync(cartItem);
        return Ok(cartItem.Adapt<CartItemView>());
    }

    
    [Authorize]
    [HttpDelete("/cart/items/{cartItemId:int}")]
    public async Task<IActionResult> DeleteCartItem(int cartItemId, [FromClaim("CartId")]int cartId)
    {
        var found = await _work.Get<CartItem>().Find(c => c.CartId == cartId && c.Id == cartItemId).FirstOrDefaultAsync();
        if (found == null)
        {
            return BadRequest($"Cart Item {cartItemId} not found!!!");
        }
        await _work.Get<CartItem>().RemoveAsync(found);
        return Ok(found);
    }
    
    [Authorize]
    [HttpPut("/cart/items/{cartItemId:int}")]
    public async Task<IActionResult> UpdateCartItem(int cartItemId, UpdateCartItem updateCartItem)
    {
        var cartId = int.Parse(User.FindFirstValue("CartId"));
        var itemFound = await _work.Get<CartItem>().Find(c => c.CartId == cartId && c.Id == cartItemId).FirstOrDefaultAsync();
        if (itemFound == null) return BadRequest($"Cart Item {updateCartItem} not found!!!");
        itemFound.Quantity = updateCartItem.Quantity;
        return Ok(itemFound);
    }
    
    [Authorize]
    [HttpPost("/cart/place-order")]
    public async Task<IActionResult> PlaceOrder(int cartItemId, UpdateCartItem updateCartItem, [FromClaim("CustomerId")]int cusId)
    {
        //Get User Cart
        var cartId = int.Parse(User.FindFirstValue("CartId"));
        var cart = await _work.Get<Cart>().GetAsync(cartId);
        if (!cart!.CartItems.Any()) return BadRequest("Cart did not have any item");
        
        //create order for the id
        var order = new Order()
        {
            CustomerId = cartId,
            TotalPrice = 0D
        };
        await _work.Get<Order>().AddAsync(order);

        double total = 0;
        //create order item
        var orderItems = new List<OrderItem>();
        foreach (var items in cart!.CartItems)
        {
            for (var i = 0; i < items.Quantity; i++)
            {
                orderItems.Add(new OrderItem
                {
                    PriceId = items.PriceId,
                    OrderProductId = items.ProductId,
                    OrderId = order.Id,
                    Price = items.Price!
                });
                total += items.Price!.Price;
            }
        }
        order.TotalPrice = total;
        await _work.Get<OrderItem>().AddAllAsync(orderItems);

        await _work.Get<CartItem>().RemoveAllAsync(cart.CartItems);
        return Ok(await _work.Get<Order>().Find<OrderView>(o => o.Id == order.Id).FirstOrDefaultAsync());
    }

    [HttpGet("{id:int}/orders")]
    public async Task<IActionResult> GetOrder(int id, PagingRequest paging, string? orderBy)
    {
        var orders = await _orderService.GetOrdersByCustomerId(id, paging, orderBy.ToOrderRequest<Order>());
        return Ok(orders.ToPagingResponse(paging));
    }
}