﻿using System.ComponentModel.DataAnnotations;
using System.Net;
using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Response;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.CartDto;
using PhuQuocVoucher.Business.Dtos.CartItemDto;
using PhuQuocVoucher.Business.Dtos.CustomerDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Dtos.RankDto;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;
using Swashbuckle.AspNetCore.Annotations;

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

    private readonly IKpiService _kpiService;

    public SellerController(ISellerService sellerService, ILogger<SellerController> logger, IUnitOfWork work, IOrderService orderService, ICustomerService customerService, ICartService cartService, IKpiService kpiService)
    {
        _sellerService = sellerService;
        _logger = logger;
        _work = work;
        _orderService = orderService;
        _customerService = customerService;
        _cartService = cartService;
        _kpiService = kpiService;
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
    [SwaggerResponse(200,"Seller view page", typeof(PagingResponse<SellerView>))]
    public async Task<ActionResult<PagingResponse<SellerView>>> Get(
        [FromQuery] FindSeller request,
        [FromQuery] PagingRequest paging,
        [FromQuery] string? orderBy,
        [FromQuery] DateTime? completeDateLowBound,
        DateTime? kpiStartDate, DateTime? kpiEndDate)
    {
        var (sellerViews, total) = await _sellerService.FindSellerAsync(new GetRequest<Seller>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Seller>(),
            PagingRequest = paging
        }, completeDateLowBound);

        var sellerIds = sellerViews.Select(s => s.Id).ToList();

        var sellerKpis = await _kpiService.GetSellerKpi(sellerIds, kpiStartDate, kpiEndDate);
        
        foreach (var seller in sellerViews)
        {
            sellerKpis.TryGetValue(seller.Id , out var kpi);
            seller.Kpi = kpi ?? new SellerKPI();
        }

        return Ok((sellerViews, total).ToPagingResponse(paging));
    }

    [HttpPost]
    [SwaggerResponse(200,"Seller", typeof(Seller))]
    public async Task<ActionResult<Seller>> Create([FromBody] CreateSeller request)
    {
        return Ok(await _sellerService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    [SwaggerResponse(200,"Seller view", typeof(SellerView))]
    public async Task<ActionResult<SellerView>> Get(int id)
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
    [SwaggerResponse(200,"Seller view", typeof(SellerView))]
    public async Task<ActionResult<SellerView>> GetCurrent([FromClaim("SellerId")]int id)
    {
        var seller = await _repo.Find<SellerView>(seller => seller.Id == id).FirstOrDefaultAsync() ??
                                  throw new ModelNotFoundException($"Not Found {nameof(Seller)} with id {id}");
        var nextRank = await _work.Get<SellerRank>().Find<RankView>(rank => rank.EpxRequired >= seller.Exp).OrderBy(rank => rank.EpxRequired).FirstOrDefaultAsync();
        seller.NextRank = nextRank;
        return Ok(seller);
    }
    

    [HttpPut("{id:int}")] 
    [SwaggerResponse(200,"Seller", typeof(Seller))]
    public async Task<ActionResult<Seller>> Update([FromBody] UpdateSeller request, int id)
    {
        return Ok(await _sellerService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    [SwaggerResponse(200,"Seller", typeof(Seller))]
    public async Task<ActionResult<Seller>> Delete(int id)
    {
        return Ok(await _sellerService.DeleteAsync(id));
    }

    
    /// <summary>
    /// Create a customer
    /// </summary>
    /// <param name="request">Create customer from</param>
    /// <param name="sellerId"></param>
    /// <returns>Customer newly created</returns>
    [Authorize(Roles = nameof(Role.Seller))]
    [SwaggerResponse(200,"Cart view", typeof(CustomerView))]
    [HttpPost("customers")]
    public async Task<ActionResult<CustomerView>> CreateCustomer([FromBody]CreateCustomer request, [FromClaim("SellerId")] int sellerId)
    {
        request.UserInfo.Role = Role.Customer;
        return Ok(await _customerService.CreateCustomerAsync(request, sellerId));
    }
    
    /// <summary>
    /// Get Customer List
    /// </summary>
    /// <param name="sellerId">Get from claims</param>
    /// <returns>all customers  this seller support</returns>
    [Authorize(Roles = nameof(Role.Seller))]
    [SwaggerResponse(200,"List of customers been assigned to you", typeof(List<CustomerView>))]
    [HttpGet("customers")]
    public async Task<ActionResult<List<CustomerView>>> GetCustomer([FromClaim("SellerId")] int? sellerId)
    {
        return Ok(await _work.Get<Customer>().Find<CustomerView>(c => c.AssignSellerId == sellerId).ToListAsync());
    }
    
    
    
    /// <summary>
    /// Add an cart item
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <param name="customerId">Customer id</param>
    /// <returns>Customer cart</returns>
    [HttpPost("customers/{customerId:int}/cart/items/")]
    [SwaggerResponse(200,"Cart view", typeof(CartView))]
    [Authorize(Roles = nameof(Role.Seller))]
    public async Task<ActionResult<CartView>> AddCartItem(CreateCartItem item, int customerId)
    {
        return Ok(await _cartService.AddItemToCart(item, customerId));
    }
    
    
    /// <summary>
    /// Delete a cart items
    /// </summary>
    /// <param name="customerId">Customer id</param>
    /// <param name="cartItemId">Cart Id</param>
    /// <returns>Cart</returns>
    [Authorize(Roles = nameof(Role.Seller))]
    [SwaggerResponse(200,"Cart view", typeof(CartView))]
    [HttpDelete("customers/{customerId:int}/cart/items/{cartItemId:int}")]
    public async Task<ActionResult<CartView?>> DeleteCartItem(int customerId, int cartItemId)
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
    /// update cart item quantity to customer cart
    /// </summary>
    /// <returns>CartItemView</returns>
    [Authorize(Roles = nameof(Role.Seller))]
    [HttpPut("customers/{customerId:int}/cart/items")]
    public async Task<ActionResult<CartView>> UpdateCartItem(
        IList<UpdateCartItem> updateCartItem, int customerId)
    {
        var cart = await _cartService.GetCartByCustomerAsync(customerId);
        return Ok(await _cartService.UpdateCartItems(updateCartItem, cart.Id, customerId));
    }
    
    /// <summary>
    /// clear cart item of customer cart
    /// </summary>
    /// <returns>CartItemView</returns>
    [Authorize(Roles = nameof(Role.Seller))]
    [HttpPut("customers/{customerId:int}/cart/clear")]
    public async Task<ActionResult<CartView>> ClearCart(int customerId)
    {
        var cart = await _cartService.GetCartByCustomerAsync(customerId);
        await _cartService.ClearCart(cart.Id);
        return Ok();
    }


    /// <summary>
    /// CLEAR cart then add all new cart item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpPut("customers/{customerId:int}/cart")]
    [Authorize(Roles = nameof(Role.Seller))]
    public async Task<ActionResult<CartView>> UpdateCart(UpdateCart item, int customerId)
    {
        return Ok(await _cartService.UpdateCartAsync(item, customerId));
    }
    
    /// <summary>
    /// Change quantity of a cart item
    /// </summary>
    /// <param name="cartItemId"></param>
    /// <param name="updateCartItem"></param>
    /// <param name="customerId"></param>
    /// <returns>cart</returns>
    [Authorize(Roles = nameof(Role.Seller))]
    [SwaggerResponse(200, "Cart view",typeof(CartView))]
    [HttpPut("customers/{customerId:int}/cart/items/{cartItemId:int}")]
    public async Task<ActionResult<CartView>> UpdateCartItem(int cartItemId, UpdateCartItem updateCartItem, int customerId)
    {
        var cart = await _cartService.GetCartByCustomerAsync(customerId);
        var itemFound = await _work.Get<CartItem>().Find(c => c.CartId == cart.Id && c.Id == cartItemId).FirstOrDefaultAsync();
        if (itemFound == null) return BadRequest($"Cart Item {updateCartItem} not found!!!");
        itemFound.Quantity = updateCartItem.Quantity;
        await _work.CompleteAsync();
        return Ok(await _cartService.GetCartByCustomerAsync(customerId));
    }
    
    /// <summary>
    /// Get cart of a customer
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpGet("customers/{customerId:int}/cart")]
    [SwaggerResponse(200, "Cart view", typeof(CartView))] 
    [Authorize(Roles = nameof(Role.Seller))]
    public async Task<ActionResult<CartView>> GetCart(int customerId)
    {
        return Ok(await _cartService.GetCartByCustomerAsync(customerId));
    }
    
    /// <summary>
    /// Create an order for a customer using their cart, cart items is delete when done
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="sellerId"></param>
    /// <returns></returns>
    [Authorize(Roles = nameof(Role.Seller))]
    [HttpPost("customers/{customerId:int}/place-order")]
    [SwaggerResponse(200, "Order View",typeof(OrderView))]
    public async Task<ActionResult<OrderView>> PlaceOrder(int customerId, [FromClaim("SellerId")]int? sellerId)
    {
        //Get User Cart
        var cart = await _cartService.GetCartByCustomerAsync(customerId);
        if (!cart.CartItems.Any()) return BadRequest("Cart did not have any item");
        return Ok(await _orderService.PlaceOrderAsync(cart, cart.CustomerId, sellerId));
    }
    
    [Authorize(Roles = nameof(Role.Seller))]
    [SwaggerResponse(200, "Order View",typeof(OrderView))]
    [HttpPut("customers/orders/{id:int}/cancel")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        return Ok((await _orderService.CancelOrderAsync(id)));
    }
    
    
    [Authorize(Roles = nameof(Role.Seller))]
    [SwaggerResponse(200, "Order View",typeof(OrderView))]
    [HttpGet("kpi")]
    public async Task<IActionResult> KPI([FromClaim("SellerId")]int sellerId, [Range(2000, 2100)]int year)
    {
        var sellerKpiView = await _sellerService.GetSellerKpis(sellerId, year);
        List<dynamic> kpiPerMonth = new();
        
        for (int i = 1; i <= 12; i++)
        {
            sellerKpiView.RevenuesPerMonths.TryGetValue(i, out var revenues);
            sellerKpiView.CloseOrderPerMonth.TryGetValue(i, out var order);
            sellerKpiView.NoOfNewCustomerPerMonth.TryGetValue(i, out var customer);
            
            kpiPerMonth.Add(new
            {
                Month = i,
                revenues,
                order,
                customer
            });
        }
        
        return Ok(kpiPerMonth);
    }
    
    [SwaggerResponse(200, "KPI",typeof(VoucherSView))]
    [HttpGet("kpi-voucher")]
    public async Task<ActionResult<List<VoucherSView>>> KpiPerVoucher([FromClaim("SellerId")]int sellerId, DateTime? kpiStartDate, DateTime? kpiEndDate)
    {
        var voucherKpiOfSeller = await _kpiService.GetVoucherKpiOfSeller(sellerId, kpiStartDate, kpiEndDate);
        var voucherIds = voucherKpiOfSeller.Keys;
        var voucherSViews = await _work.Get<Voucher>().Find<VoucherSView>(v => voucherIds.Contains(v.Id)).ToListAsync();
        foreach (var voucher in voucherSViews)
        {
            voucherKpiOfSeller.TryGetValue(voucher.Id , out var kpi);
            voucher.Kpi = kpi ?? new VoucherKPI();
        }
        return Ok(voucherSViews);
    }
    
}