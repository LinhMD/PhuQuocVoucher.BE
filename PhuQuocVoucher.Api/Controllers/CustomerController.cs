
using System.Security.Claims;
using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Response;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.CartDto;
using PhuQuocVoucher.Business.Dtos.CartItemDto;
using PhuQuocVoucher.Business.Dtos.CustomerDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Dtos.ProfileDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]s")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    private readonly ICartService _cartService;

    private readonly IOrderService _orderService;

    private readonly IProfileService _profileService;

    private readonly IUnitOfWork _work;

    private readonly ILogger<CustomerController> _logger;

    private readonly IRepository<Customer> _repo;

    /// <summary>
    /// Create Controller.
    /// </summary>
    public CustomerController(ILogger<CustomerController> logger, IUnitOfWork work,
        ICustomerService customerService, IOrderService orderService, ICartService cartService, IProfileService profileService)
    {
        _customerService = customerService;
        _logger = logger;
        _work = work;
        _orderService = orderService;
        _cartService = cartService;
        _profileService = profileService;
        _repo = work.Get<Customer>();
    }

    /// <summary> with filter
    /// Get customer.
    /// </summary>
    /// <param name="orderBy">format: [fieldName1]-[asc|dec],[fieldName2]-[asc|dec],...</param>
    /// <returns>Customer with matching filter</returns>
    /// <response code="200">Return List of Customer matching filter</response>
    /// <response code="400">If the fail validation</response>
    /// <response code="500">If im bad</response>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]FindCustomer request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        
        return Ok((await _customerService.GetAsync<CustomerView>(new GetRequest<Customer>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Customer>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    /// <summary>
    /// Create Customer, Admin used only
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<ActionResult<CustomerSView>> Create([FromBody]CreateCustomer request)
    {
        request.UserInfo.Role = Role.Customer;
        return Ok(await _customerService.CreateCustomerAsync(request));
    }

    /// <summary>
    /// Get Customer with Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ModelNotFoundException"></exception>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerSView>> Get(int id)
    {
        return Ok(await _repo.Find<CustomerSView>(cus => cus.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Customer)} with id {id}"));
    }

    /// <summary>
    /// Update Customer, null fill will not be update
    /// </summary>
    /// <param name="request"></param>
    /// <param name="id"></param>
    /// <returns>Customer</returns>
    [HttpPut("{id:int}")]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<ActionResult<Customer>> Update([FromBody] UpdateCustomer request, int id)
    {
        return Ok(await _customerService.UpdateAsync(id, request));
    }
    

    /// <summary>
    /// Delete customer with {id}
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Customer been delete</returns>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<ActionResult<Customer>> Delete(int id)
    {
        return Ok(await _customerService.DeleteAsync(id));
    }

    /// <summary>
    /// Get customer Order
    /// </summary>
    /// <param name="id"></param>
    /// <param name="paging"></param>
    /// <param name="orderBy"></param>
    /// <returns>OrderViews</returns>
    [HttpGet("{id:int}/orders")]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<ActionResult<PagingResponse<OrderView>>> GetOrder(int id,[FromQuery] PagingRequest paging, string? orderBy)
    {
        var orders = await _orderService.GetOrdersByCustomerId(id, paging, orderBy.ToOrderRequest<Order>());
        return Ok(orders.ToPagingResponse(paging));
    }
    
    /// <summary>
    /// Get customer Profiles
    /// </summary>
    /// <param name="id"></param>
    /// <param name="paging"></param>
    /// <param name="orderBy"></param>
    /// <returns>OrderViews</returns>
    [HttpGet("{id:int}/profiles")]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<ActionResult<IList<Profile>>> GetProfiles(int id)
    {
        var orders = await _profileService.GetProfileOfCustomer(id);
        return Ok(orders);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("cart")]
    [Authorize(Roles = nameof(Role.Customer))]
    public async Task<ActionResult<CartView>> GetCart([FromClaim("CustomerId")]int id)
    {
        var cart = await _work.Get<Cart>()
            .Find<CartView>(cus => cus.CustomerId == id).FirstOrDefaultAsync() ?? 
                   (await _cartService.CreateAsync(new CreateCart
        {
            CustomerId = id
        })).Adapt<CartView>();
        return Ok(cart);
    }
    
    /// <summary>
    /// clean all cart item, then add new cart item 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpPut("cart")]
    [Authorize(Roles = nameof(Role.Customer))]
    public async Task<ActionResult<CartView>> UpdateCart([FromBody]UpdateCart item, [FromClaim("CustomerId")] int customerId)
    {
        return Ok(await _cartService.UpdateCartAsync(item, customerId));
    }

    
    /// <summary>
    /// Add product item to login customer cart
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cusId"></param>
    /// <returns></returns>
    [HttpPost("cart/items")]
    [Authorize(Roles = nameof(Role.Customer))]
    public async Task<ActionResult<CartView>> AddCartItem(CreateCartItem item, [FromClaim("CustomerId")]int cusId)
    {
        return Ok(await _cartService.AddItemToCart(item, cusId));
    }

    /// <summary>
    /// CLEAR cart then add all new cart item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cusId"></param>
    /// <returns></returns>
    [HttpPut("cart/items/all")]
    [Authorize(Roles = nameof(Role.Customer))]
    public async Task<ActionResult<CartView>> AddCartItem(UpdateCart item, [FromClaim("CustomerId")]int cusId)
    {
        return Ok(await _cartService.UpdateCartAsync(item, cusId));
    }
    
    /// <summary>
    /// delete product item from login customer cart
    /// </summary>
    /// <returns>CartItemView</returns>
    [Authorize(Roles = nameof(Role.Customer))]
    [HttpDelete("cart/items/{cartItemId:int}")]
    public async Task<ActionResult<CartItemView>> DeleteCartItem(int cartItemId, [FromClaim("CartId")]int cartId)
    {
        var found = await _work.Get<CartItem>()
            .Find(c => c.CartId == cartId && c.Id == cartItemId)
            .FirstOrDefaultAsync();
        if (found == null)
        {
            return BadRequest($"Cart Item {cartItemId} not found!!!");
        }
        await _work.Get<CartItem>().RemoveAsync(found);
        return Ok(found.Adapt<CartItemView>());
    }
    
    /// <summary>
    /// update cart item quantity to login customer cart
    /// </summary>
    /// <returns>CartItemView</returns>
    [Authorize(Roles = nameof(Role.Customer))]
    [HttpPut("cart/items")]
    public async Task<ActionResult<CartView>> UpdateCartItem(
        IList<UpdateCartItem> updateCartItem, 
        [FromClaim("CartId")]int cartId,
        [FromClaim("CustomerId")] int customerId)
    {
        return Ok(await _cartService.UpdateCartItems(updateCartItem, cartId, customerId));
    }
    
    /// <summary>
    /// clear cart item of customer cart
    /// </summary>
    /// <returns>CartItemView</returns>
    [Authorize(Roles = nameof(Role.Customer))]
    [HttpPut("cart/clear")]
    public async Task<ActionResult<CartView>> ClearCart([FromClaim("CustomerId")]int customerId)
    {
        var cart = await _cartService.GetCartByCustomerAsync(customerId);
        await _cartService.ClearCart(cart.Id);
        return Ok();
    }
    /// <summary>
    /// Create an order from Login Customer Cart, then remove all cart item
    /// </summary>
    /// <returns>OrderView</returns>
    [Authorize(Roles = nameof(Role.Customer))]
    [HttpPost("cart/place-order")]
    public async Task<ActionResult<OrderView>> PlaceOrder([FromClaim("CustomerId")]int cusId, [FromClaim("CartId")]  int cartId)
    {
        //Get User Cart
        var cart = await _cartService.GetCartByCustomerAsync(cusId);
     
        if (!cart!.CartItems.Any()) return BadRequest("Cart did not have any item");
      
        return Ok(await _orderService.PlaceOrderAsync(cart, cusId));
    }

    
    /// <summary>
    /// Get order of login Customer
    /// </summary>
    /// <returns>PagingResponse<OrderView></returns>
    [Authorize(Roles = nameof(Role.Customer))]
    [HttpGet("orders")]
    public async Task<ActionResult<PagingResponse<OrderView>>> GetOrder([FromQuery] FindOrder request, [FromQuery] PagingRequest paging, string? orderBy, [FromClaim("CustomerId")]int customerId)
    {
        request.CustomerId = customerId;
        
        return Ok((await _orderService.GetAsync<OrderView>(new GetRequest<Order>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Order>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }
    /// <summary>
    /// Update profile of order item
    /// </summary>
    /// <returns>OrderView</returns>
    [Authorize(Roles = nameof(Role.Customer))]
    [HttpPut("orders/{orderId:int}/items/{itemId:int}/")]
    public async Task<ActionResult<OrderView>> UpdateOrderItem([FromClaim("CustomerId")]int customerId, int itemId, int orderId,[FromBody] int profileId)
    {
        var order = await _work.Get<Order>().GetAsync(orderId);
        if (order is null || order.CustomerId != customerId)
            throw new ModelNotFoundException($"Order not found with id: {orderId}");
        var orderItems = order.OrderItems.FirstOrDefault(item => item.Id == itemId);

        if (orderItems is null) throw new ModelNotFoundException($"Order item not found with id: {orderId}");
        orderItems.ProfileId = profileId;
        await _work.CompleteAsync();
        return Ok(await _work.Get<Order>().GetAsync<OrderView>(orderId));
    }
    
    /// <summary>
    /// Get Current Login Customer
    /// </summary>
    /// <param name="id"></param>
    /// <returns>CustomerSView</returns>
    /// <exception cref="ModelNotFoundException"></exception>
    [HttpGet("current")]
    [Authorize(Roles = nameof(Role.Customer))]
    public async Task<ActionResult<CustomerSView>> GetCurrent([FromClaim("CustomerId")]int id)
    {
        return Ok(await _repo.Find<CustomerView>(cus => cus.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Customer)} with id {id}"));
    }
    
    /// <summary>
    /// Get login customer Profiles
    /// </summary>
    /// <param name="id"></param>
    /// <param name="paging"></param>
    /// <param name="orderBy"></param>
    /// <returns>OrderViews</returns>
    [HttpGet("profiles")]
    [Authorize(Roles = nameof(Role.Customer))]
    public async Task<ActionResult<IList<ProfileView>>> GetCurrentProfiles([FromClaim("CustomerId")]int id)
    {
        var orders = await _profileService.GetProfileOfCustomer(id);
        return Ok(orders);
    }
    
    /// <summary>
    /// create a profile
    /// </summary>
    /// <param name="request"></param>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [Authorize(Roles = nameof(Role.Customer))]
    [HttpPost("profiles")]
    public async Task<ActionResult<ProfileView>> CreateProfile([FromBody] CreateProfile request, [FromClaim("CustomerId")] int customerId)
    {
        request.CustomerId = customerId;
        return Ok((await _profileService.CreateAsync(request)).Adapt<ProfileView>());
    }

    /// <summary>
    /// get a profile with {id} of login customer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="customerId"></param>
    /// <returns></returns>
    /// <exception cref="ModelNotFoundException"></exception>
    [Authorize(Roles = nameof(Role.Customer))]
    [HttpGet("profiles/{id:int}")]
    public async Task<IActionResult> GetProfile(int id, [FromClaim("CustomerId")] int customerId)
    {
        return Ok(await _work.Get<Profile>().Find(profile => profile.Id == id && profile.CustomerId == customerId).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Profile)} with id {id}"));
    }

    
    [Authorize(Roles = nameof(Role.Customer))]
    [HttpPut("profiles/{id:int}")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfile request, int id, [FromClaim("CustomerId")] int customerId)
    {
        var profile = await _work.Get<Profile>().Find(profile => profile.Id == id && profile.CustomerId == customerId)
            .FirstOrDefaultAsync();
        if (profile == null)
            return NotFound($"Profile with id {id} not found");
        return Ok((await _profileService.UpdateAsync(id, request)).Adapt<ProfileView>());
    }

    [Authorize(Roles = nameof(Role.Customer))]
    [HttpDelete("profiles/{id:int}")]
    public async Task<IActionResult> DeleteProfile(int id,  [FromClaim("CustomerId")] int customerId)
    {
        var profile = await _work.Get<Profile>().Find(profile => profile.Id == id && profile.CustomerId == customerId)
            .FirstOrDefaultAsync();
        if (profile == null)
            return NotFound($"Profile with id {id} not found");

        return Ok((await _profileService.DeleteAsync(id)).Adapt<ProfileView>());
    }

    [Authorize(Roles = nameof(Role.Customer))]
    [HttpGet("voucher/review")]
    public async Task<ActionResult<bool>> IsAllowCustomerReview(int voucherId, [FromClaim("CustomerId")] int customerId)
    {
        return Ok(await _work.Get<OrderItem>()
            .Find(item => item.CustomerId == customerId && item.VoucherId == voucherId).AnyAsync());
    }
    
    [Authorize(Roles = nameof(Role.Customer))]
    [HttpGet("cart/check")]
    public async Task<ActionResult<IList<RemainVoucherInventory>>> CheckCart([FromClaim("CustomerId")] int customerId)
    {
        return Ok(await _cartService.CheckCart(customerId));
    }
}