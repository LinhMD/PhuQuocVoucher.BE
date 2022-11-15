using System.Drawing;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Mapster;
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
    private IMailingService _mailingService;

    public OrderController(IOrderService orderService, ILogger<OrderController> logger, IUnitOfWork work, IMailingService mailingService)
    {
        _orderService = orderService;
        _logger = logger;
        _work = work;
        _mailingService = mailingService;
        _repo = work.Get<Order>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindOrder request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _orderService.GetAsync<OrderSView>(new GetRequest<Order>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Order>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrder request)
    {
        return Ok(await _orderService.CreateOrderAsync(request));
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

    [HttpGet("notify-user")]
    public async Task<IActionResult> SendOrderInfoToCustomerEmail(int orderId)
    {
        var order = await _work.Get<Order>().Find<OrderView>(o => o.Id == orderId && o.OrderStatus == OrderStatus.Completed).FirstOrDefaultAsync();
        if (order == null) return NotFound($"Not found Complete payment Order with id {orderId}");
        
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        var list = new List<dynamic>();
        foreach (var item  in order.OrderItems)
        {
            var hash = item.QrCode.HashCode;
            var priceName = item.Price.PriceLevelName;
            var solePrice = item.SoldPrice;
            var voucherName = item.VoucherName;
            
            QRCodeData qrCodeData =
                qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            var qrCodeBitMapByte = qrCode.GetGraphic(20);
            using var qrCodeSteam = new MemoryStream(qrCodeBitMapByte);
            string base64String = Convert.ToBase64String(qrCodeSteam.ToArray());
            var qrcode = base64String;
            list.Add(new
            {
                hash,priceName,solePrice,voucherName, qrcode
            });
        }

        var qrcodes = list.Aggregate("", (s, o) =>
        {
            return s +=$"<tr><table><tbody><tr><img src='data:image/png;base64,{o.qrcode}' width='180' height='280' class='CToWUd' data-bit='iit'></tr><tr>{o.voucherName}</tr><tr>{o.priceName}</tr><tr>{o.solePrice}</tr></tbody></table></tr>";
        });
        var paymentDetailId = order.PaymentDetail!.Id;

        var userId = ((await _work.Get<PaymentDetail>().GetAsync(paymentDetailId))!).UserId;
        var customerName = ((await _work.Get<User>().GetAsync(userId ?? 0))!).UserName;
        await _mailingService.SendEmailAsync(new MailTemplateRequest()
        {
            values = new Dictionary<string, string>()
            {
                {"username", customerName},
                {"QrCodes", qrcodes}
            },
            MailRequest = new MailRequest()
            {
                Subject = "test mail",
                ToEmail = "linhmaidinh1@gmail.com"
            },
            FileTemplateName = "QRCodeResponse"
        });
        return Ok();
    }

}