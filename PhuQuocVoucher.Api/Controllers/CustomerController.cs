using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        return Ok(await _customerService.CreateAsync(request));
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
    public async Task<IActionResult> GetCart(int id)
    {
        var cart = await _work.Get<Cart>().Find(cus => cus.CustomerId == id).FirstOrDefaultAsync() ??
                   await _cartService.CreateAsync(new CreateCart
        {
            CustomerId = id
        });
        return Ok(cart);
    }


    [HttpGet("{id:int}/orders")]
    public async Task<IActionResult> GetOrder(int id, PagingRequest paging, string? orderBy)
    {
        var orders = await _orderService.GetOrdersByCustomerId(id, paging, new OrderRequest<Order>());
        return Ok(orders.ToPagingResponse(paging));
    }
}