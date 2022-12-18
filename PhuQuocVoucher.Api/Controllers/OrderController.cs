using System.Drawing;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.Ultility;
using PhuQuocVoucher.Business.Dtos.MailDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;
using QRCoder;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    private readonly ILogger<OrderController> _logger;

    private readonly IRepository<Order> _repo;

    private readonly IUnitOfWork _work;
    
    private readonly IMailingService _mailingService;

    public OrderController(IOrderService orderService, ILogger<OrderController> logger, IUnitOfWork work, IMailingService mailingService)
    {
        _orderService = orderService;
        _logger = logger;
        _work = work;
        _mailingService = mailingService;
        _repo = work.Get<Order>();
    }

    [HttpGet]
    public async Task<ActionResult<OrderSView>> Get([FromQuery] FindOrder request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _orderService.GetAsync<OrderSView>(new GetRequest<Order>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Order>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find<OrderView>(order => order.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Order)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateOrder request, int id)
    {
        return Ok((await _orderService.UpdateAsync(id, request)).Adapt<OrderView>());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok((await _orderService.DeleteAsync(id)).Adapt<OrderView>());
    }
    
    [HttpPut("{id:int}/cancel")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        return Ok((await _orderService.CancelOrderAsync(id)));
    }

    
    [HttpGet("{id:int}/print")]
    public async Task<IActionResult> PrintOrder(int id)
    {
        return Ok((await _orderService.RenderOrderToHtml(await _repo.Find(o => o.Id == id).FirstOrDefaultAsync() ?? throw new ModelNotFoundException($"Order not found with Id {id}"))));
    }
    
    [HttpGet("{id:int}/send-email")]
    public async Task<IActionResult> SendEmailOrder(int id)
    {
        return Ok(await _orderService.SendOrderEmailToCustomer(id));
    }
}