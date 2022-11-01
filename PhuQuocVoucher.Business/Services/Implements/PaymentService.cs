using System.Globalization;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PhuQuocVoucher.Business.Dtos.MomoDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class PaymentService
{
    private readonly IUnitOfWork _work;
    private readonly MomoSetting _momoSetting;

    public PaymentService(MomoSetting momoSetting, IUnitOfWork work)
    {
        _momoSetting = momoSetting;
        _work = work;
    }

    public async Task<MomoResponse> CreatePaymentRequest(int orderId, bool isMobile = false)
    {
        var order = await _work.Get<Order>().GetAsync(orderId);
        if (order == null) throw new ModelNotFoundException($"Order not found with id {orderId}");

        if (order.OrderStatus == OrderStatus.Completed || 
            order.OrderStatus == OrderStatus.Used ||
            order.OrderStatus == OrderStatus.Canceled)
            throw new ModelValueInvalidException("Can not create payment request for order " + orderId);
        var endpoint = _momoSetting.EndPoint;
        var partnerCode = _momoSetting.PartnerCode;
        var accessKey = _momoSetting.AccessKey;
        var secretKey = _momoSetting.SecretKey;
        var orderInfo = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + order.Id;
        var redirectUrl = isMobile? "https://www.citydiscovery.tech/thank/" : "";
        var ipnUrl = "https://citytourist.azurewebsites.net/api/v1/momo/callback";
        var requestType = "captureWallet";
        
        /*
        if (request.IsMobile) redirectUrl = "https://citydiscovertourist.page.link/homepage";
        */
        
        var amount = order.TotalPrice.ToString(CultureInfo.InvariantCulture);
        var requestId = Guid.NewGuid();
        var extraData = "";

        var rawHash = "accessKey=" + accessKey + "&amount=" + amount + "&extraData=" + extraData + "&ipnUrl=" + ipnUrl +
                      "&orderId=" + orderId + "&orderInfo=" + orderInfo + "&partnerCode=" + partnerCode +
                      "&redirectUrl=" + redirectUrl + "&requestId=" + requestId + "&requestType=" + requestType;

        var signature = MoMoSecurity.SignSha256(rawHash, secretKey!);

        var message = new JObject
        {
            { "partnerCode", partnerCode },
            { "partnerName", "Phu Quoc Company" },
            { "storeId", "Phu QuocVoucher" },
            { "requestId", requestId },
            { "amount", amount },
            { "orderId", orderId },
            { "orderInfo", orderInfo },
            { "redirectUrl", redirectUrl },
            { "ipnUrl", ipnUrl },
            { "lang", "en" },
            { "autoCapture", false},
            { "extraData", extraData },
            { "requestType", requestType },
            { "signature", signature }
        };
        
        var response = await PaymentRequest.SendPaymentRequestAsync(endpoint!, message.ToString());
        var jMessage = JObject.Parse(response);
        Console.WriteLine(jMessage.ToString());
        var momoResponse = jMessage.ToObject<MomoResponse>();
        if (momoResponse == null) 
            throw new ModelValueInvalidException("Can not create a payment request");

        order.PaymentRequestId = requestId;
        order.OrderStatus = OrderStatus.Processing;
        await _work.CompleteAsync();
        return momoResponse;
    }

    public async Task ConfirmPayment(MomoIPNRequest request)
    {
        var orderId = request.OrderId;
        var order = await _work.Get<Order>().GetAsync(orderId);
        
        
        
        
    }
}