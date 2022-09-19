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
using PhuQuocVoucher.Business.Dtos.CartDto;
using PhuQuocVoucher.Business.Dtos.CartItemDto;
using PhuQuocVoucher.Business.Dtos.CustomerDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class SellerController : ControllerBase
{
    private readonly ISellerService _sellerService;

    private readonly IOrderService _orderService;

    private readonly ICustomerService _customerService;
    
    private readonly ICartService _cartService;

    private readonly ILogger<SellerController> _logger;

    private readonly IRepository<Seller> _repo;

    private readonly IUnitOfWork _work;
    public SellerController(ISellerService sellerService, ILogger<SellerController> logger, IUnitOfWork work, IOrderService orderService, ICustomerService customerService, ICartService cartService)
    {
        _sellerService = sellerService;
        _logger = logger;
        _work = work;
        _orderService = orderService;
        _customerService = customerService;
        _cartService = cartService;
        _repo = work.Get<Seller>();
    }

    /// <summary>
    /// GET list of seller matching filter criteria
    /// </summary>
    /// <param name="request">Filter condition</param>
    /// <param name="paging">For pagination</param>
    /// <param name="orderBy">Format: [fieldName1]-[asc|dec],[fieldName2]-[asc|dec],... .Allows:[Id|CommissionRate|SellerName|BusyLevel]</param>
    /// <param name="completeDateLowBound">profit from {param} to current date</param>
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] FindSeller request,
        [FromQuery] PagingRequest paging,
        [FromQuery] string? orderBy,
        [FromQuery] DateTime? completeDateLowBound)
    {
        return Ok((await _sellerService.FindSellerAsync(new GetRequest<Seller>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Seller>(),
            PagingRequest = paging
        }, completeDateLowBound)).ToPagingResponse(paging));

    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSeller request)
    {
        return Ok(await _sellerService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find<SellerView>(seller => seller.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Seller)} with id {id}"));
    }
    
    /// <summary>
    /// Get current login seller info 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Seller Information</returns>
    ///<code></code>
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent([FromClaim("SellerId")]int id)
    {
        return Ok(await _repo.Find<SellerView>(seller => seller.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Seller)} with id {id}"));
    }
    

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateSeller request, int id)
    {
        return Ok(await _sellerService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _sellerService.DeleteAsync(id));
    }

    /// <summary>
    /// Create an order for customer,
    /// required seller authorization
    /// </summary>
    /// <param name="createOrder">Order being create</param>
    /// <param name="sellerId">nah this field get from jwt, better be authenticated :v</param>
    /// <returns>Order have been successfully created</returns>
    [Authorize]
    [HttpPost("order")]
    public async Task<IActionResult> CreateOrder(CreateOrder createOrder, [FromClaim("SellerId")] int sellerId)
    {
        createOrder.SellerId = sellerId;
        return Ok(await _orderService.CreateOrderAsync(createOrder));
    }
    
    /// <summary>
    /// Create a customer
    /// </summary>
    /// <param name="request">Create customer from</param>
    /// <param name="sellerId"></param>
    /// <returns>Customer newly created</returns>
    [Authorize]
    [HttpPost("customers")]
    public async Task<IActionResult> CreateCustomer([FromBody]CreateCustomer request, [FromClaim("SellerId")] int sellerId)
    {
        request.UserInfo.Role = Role.Customer;
        return Ok(await _customerService.CreateCustomerAsync(request));
    }
    
    /// <summary>
    /// Add an cart item
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <param name="customerId">Customer id</param>
    /// <returns>Customer cart</returns>
    [HttpPost("customers/{customerId:int}/cart/items/")]
    [Authorize]
    public async Task<IActionResult> AddCartItem(CreateCartItem item, int customerId)
    {
        var cart = await _cartService.GetCartByCustomerAsync(customerId);
        var cartItem = item.Adapt<CartItem>();
        cartItem.CartId = cart.Id;
        var itemView = (await _work.Get<CartItem>().AddAsync(cartItem)).Adapt<CartItemView>();
        cart.CartItems.Add(itemView);
        return Ok(cart);
    }
    
    /// <summary>
    /// Delete a cart items
    /// </summary>
    /// <param name="customerId">Customer id</param>
    /// <param name="cartItemId">Cart Id</param>
    /// <returns>Cart</returns>
    [Authorize]
    [HttpDelete("customers/{customerId:int}/cart/items/{cartItemId:int}")]
    public async Task<IActionResult> DeleteCartItem(int customerId, int cartItemId)
    {
        var cart = await _cartService.GetCartByCustomerAsync(customerId);
        var found = cart.CartItems.FirstOrDefault(c => c.Id == cartItemId);
        
        if (found == null)
            return BadRequest($"Cart Item {cartItemId} not found!!!");
        
        await _work.Get<CartItem>().RemoveAsync(new CartItem{Id = found.Id});
        cart.CartItems.Remove(found);
        return Ok(cart);
    }
    
    /// <summary>
    /// Change quantity of a cart item
    /// </summary>
    /// <param name="cartItemId"></param>
    /// <param name="updateCartItem"></param>
    /// <param name="customerId"></param>
    /// <returns>cart</returns>
    [Authorize]
    [HttpPut("customers/{customerId:int}/cart/items/{cartItemId:int}")]
    public async Task<IActionResult> UpdateCartItem(int cartItemId, UpdateCartItem updateCartItem, int customerId)
    {
        var cart = await _cartService.GetCartByCustomerAsync(customerId);
        var itemFound = await _work.Get<CartItem>().Find(c => c.CartId == cart.Id && c.Id == cartItemId).FirstOrDefaultAsync();
        if (itemFound == null) return BadRequest($"Cart Item {updateCartItem} not found!!!");
        itemFound.Quantity = updateCartItem.Quantity;
        return Ok(itemFound.Adapt<CartItemView>());
    }
    
    /// <summary>
    /// Get cart of a customer
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpGet("customers/{customerId:int}/cart")]
    [Authorize]
    public async Task<IActionResult> GetCart(int customerId)
    {
        var cart = (await _cartService.GetCartByCustomerAsync(customerId)).Adapt<CartView>();
        return Ok(cart);
    }
    
    /// <summary>
    /// Create an order for a customer using their cart, cart items is delete when done
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="sellerId"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("customers/{customerId:int}/place-order")]
    public async Task<IActionResult> PlaceOrder(int customerId, [FromClaim("SellerId")]int? sellerId)
    {
        //Get User Cart
        var cart = await _cartService.GetCartByCustomerAsync(customerId);
        if (!cart.CartItems.Any()) return BadRequest("Cart did not have any item");
        return Ok(await _orderService.PlaceOrderAsync(cart, cart.CustomerId, sellerId));
    }
}