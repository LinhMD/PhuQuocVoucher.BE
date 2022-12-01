using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.Ultility;
using PhuQuocVoucher.Business.Dtos.MomoDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Services.Implements;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IUnitOfWork _work;

    private readonly PaymentService _paymentService;

    public PaymentController(IUnitOfWork work, PaymentService paymentService)
    {
        _work = work;
        _paymentService = paymentService;
    }

    /// <summary>
    /// Get payment(QR code link, momo app link, web momo link) method for an order 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("momo/{orderId:int}")]
    public async Task<IActionResult> RequestPaymentMethod(int orderId, [FromClaim("Id")] int userId)
    {
        return Ok(await _paymentService.CreatePaymentRequest(orderId, userId));
    }

    /// <summary>
    /// for momo to call back to BE to confirm payment has been paid 
    /// </summary>
    /// <param name="callbackRequest"></param>
    /// <returns></returns>
    [HttpPost("momo/callback")]
    public async Task<IActionResult> CallBack([FromBody] MomoIPNRequest callbackRequest)
    {
        try
        {
            await _paymentService.UpdateStatusWhenSuccessAsync(callbackRequest);
        }
        catch (Exception e)
        {
            e.Message.Dump();
            e.StackTrace.Dump();
            /*var paymentId = await _work.Get<PaymentDetail>().Find(p => p.RequestId.ToString() == callbackRequest.RequestId)
                .Select(p => p.Id).FirstOrDefaultAsync();
            await _paymentService.PaymentFailed(callbackRequest.OrderId, paymentId);*/
        }
        return NoContent();
    }
}