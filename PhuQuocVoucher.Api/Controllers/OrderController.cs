using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Dtos.OrderDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/order")]
[CrudExceptionFilter]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    private readonly ILogger<OrderController> _logger;

    private readonly IRepository<Order> _repo;

    public OrderController(IOrderService orderService, ILogger<OrderController> logger, IUnitOfWork work)
    {
        _orderService = orderService;
        _logger = logger;
        _repo = work.Get<Order>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindOrder request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _orderService.GetAsync(new GetRequest<Order>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<Order>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrder request)
    {
        return Ok(await _orderService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find(order => order.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Order)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateOrder request, int id)
    {
        return Ok(await _orderService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _orderService.DeleteAsync(id));
    }
}