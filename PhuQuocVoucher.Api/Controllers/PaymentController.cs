using CrudApiTemplate.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.MomoDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaymentController : ControllerBase
{
    private IUnitOfWork _work;

    public PaymentController(IUnitOfWork work)
    {
        _work = work;
    }

    /// <summary>
    /// Get payment(QR code link, momo app link, web momo link) method for an order 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet("momo/{orderId:int}")]
    public async Task<IActionResult> RequestPaymentMethod(int orderId)
    {
        var order = await _work.Get<Order>().Find<OrderView>(o => o.Id == orderId).FirstOrDefaultAsync();
        if (order == null) return NotFound($"Order not found with id {orderId}");
        
        
        
        return Ok();
    }

    /// <summary>
    /// for momo to call back to BE to confirm payment has been paid 
    /// </summary>
    /// <param name="callbackRequest"></param>
    /// <returns></returns>
    [HttpPost("momo/callback")]
    public async Task<IActionResult> InpCallback(MomoIPNRequest callbackRequest)
    {
        
        return NoContent();
    }
}