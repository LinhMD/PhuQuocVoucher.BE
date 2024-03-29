﻿using System.Globalization;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PhuQuocVoucher.Api.Ultility;
using PhuQuocVoucher.Business.Dtos.MomoDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class PaymentService
{
    private readonly IUnitOfWork _work;
    private readonly MomoSetting _momoSetting;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public PaymentService(MomoSetting momoSetting, IUnitOfWork work, IBackgroundJobClient backgroundJobClient)
    {
        _momoSetting = momoSetting;
        _work = work;
        _backgroundJobClient = backgroundJobClient;
    }

    public async Task<MomoResponse> CreatePaymentRequest(int orderId, int userId, bool isMobile = false)
    {
        var order = await _work.Get<Order>().GetAsync(orderId);
        if (order == null) throw new ModelNotFoundException($"Order not found with id {orderId}");

        if (order.OrderStatus is OrderStatus.Completed or OrderStatus.Used or OrderStatus.Canceled)
            throw new ModelValueInvalidException("Can not create payment request for order " + orderId);
        var endpoint = _momoSetting.EndPoint;
        var partnerCode = _momoSetting.PartnerCode;
        var secretKey = _momoSetting.SecretKey;
        var orderInfo = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + order.Id;
        var redirectUrl = isMobile ? _momoSetting.MobileRedirect : _momoSetting.WebRedirect;
        const string ipnUrl = "https://webapp-221010174451.azurewebsites.net/api/v1/momo/callback";
        const string requestType = "captureWallet";


        var requestId = Guid.NewGuid();
        const string extraData = "";
        var oldPaymentId = order.PaymentDetailId;
        var oldPayment = await _work.Get<PaymentDetail>().GetAsync(oldPaymentId);
        if (oldPayment is not null)
        {
            await _work.Get<PaymentDetail>().RemoveAsync(oldPayment);
        }
        
        var payment = new PaymentDetail
        {
            OrderId = orderId,
            Status = ModelStatus.Active,
            CreateAt = DateTime.Now,
            IsValid = true,
            TotalAmount = order.TotalPrice,
            UserId = userId,
            PaymentDate = DateTime.Now,
            PaymentStatus = PaymentStatus.Pending,
            RequestId = requestId
        };

        await _work.Get<PaymentDetail>().AddAsync(payment);

        var rawHash = $"accessKey={_momoSetting.AccessKey}&amount={order.TotalPrice}&extraData={extraData}&ipnUrl={_momoSetting.IpnCallback}&orderId={payment.Id}&orderInfo={orderInfo}&partnerCode={_momoSetting.PartnerCode}&redirectUrl={redirectUrl}&requestId={requestId}&requestType={requestType}";

        var signature = MoMoSecurity.SignSha256(rawHash, secretKey!);
        
        signature.Dump();
        
        var getPaymentMethodRequest = new JObject
        {
            {"partnerCode", partnerCode},
            {"partnerName", "Phu Quoc Company"},
            {"storeId", "Phu QuocVoucher"},
            {"requestId", requestId},
            {"amount", order.TotalPrice},
            {"orderId", payment.Id},
            {"orderInfo", orderInfo},
            {"redirectUrl", redirectUrl},
            {"ipnUrl", ipnUrl},
            {"lang", "en"},
            {"autoCapture", false},
            {"extraData", extraData},
            {"requestType", "captureWallet"},
            {"signature", signature}
        };

        var response = await PaymentRequest.SendPaymentRequestAsync(endpoint!, getPaymentMethodRequest.ToString());
        var jMessage = JObject.Parse(response);
        Console.WriteLine(jMessage.ToString());
        var momoResponse = jMessage.ToObject<MomoResponse>();
        if (momoResponse == null)
            throw new ModelValueInvalidException("Can not create a payment request");

        order.PaymentRequestId = requestId;

        order.PaymentDetailId = payment.Id;
        
        order.OrderStatus = OrderStatus.Processing;
        await _work.CompleteAsync();
        _backgroundJobClient.Schedule(() => PaymentFailed(payment.Id, orderId), TimeSpan.FromMinutes(15));
        return momoResponse;
    }

    public async Task UpdateStatusWhenSuccessAsync(MomoIPNRequest request)
    {
        if (request.ResultCode != 9000)
            throw new ModelValueInvalidException("Something went wrong when processing payment");

        var orderId = request.OrderId;
        var order = await _work.Get<Order>().GetAsync(orderId);

        if (order == null)
            throw new ModelNotFoundException($"Order not found with id {request.OrderId}");

        var qrCodeIds = await _work.Get<OrderItem>().Find(item => item.OrderId == order.Id && item.QrCodeId != null)
            .Select(item => item.QrCodeId).ToListAsync();

        var qrCodeInfos = await _work.Get<QrCodeInfo>().Find(qr => qrCodeIds.Contains(qr.Id)).ToListAsync();

        var paymentDetail = await _work.Get<PaymentDetail>().Find(p => p.RequestId.ToString() == request.RequestId)
            .FirstOrDefaultAsync();

        if (paymentDetail == null || paymentDetail.OrderId != orderId)
            throw new ModelValueInvalidException("");

        var momoResponse = await ConfirmPaymentAsync(paymentDetail);
        var redirectUrl = paymentDetail.User.Role == Role.Seller
            ? _momoSetting.MobileRedirect
            : _momoSetting.WebRedirect;
        
        var rawHash = $"accessKey={_momoSetting.AccessKey}&amount={paymentDetail.TotalAmount}&extraData=\"\"$&ipnUrl={_momoSetting.IpnCallback}&orderId={paymentDetail.Id}&orderInfo=\"\"&partnerCode={_momoSetting.PartnerCode}&redirectUrl={redirectUrl}&requestId={paymentDetail.RequestId}&requestType=captureWallet";
        
        var signature = MoMoSecurity.SignSha256(rawHash, _momoSetting.SecretKey!);
        Console.WriteLine("hash: " +  signature);
        Console.WriteLine("momo:" + momoResponse.Signature);
        
        /*if (signature != momoResponse.Signature)
            throw new ModelValueInvalidException("Signature is not valid");*/

        if (momoResponse.resultCode != 9000)
            throw new ModelValueInvalidException("Failed momo payment");

        if (Math.Abs(momoResponse.amount - paymentDetail!.TotalAmount ?? 0) > 0.1)
            throw new ModelValueInvalidException("Amount is not valid");

        paymentDetail.PaymentStatus = PaymentStatus.Success;
        qrCodeInfos.ForEach(qr => qr.Status = QRCodeStatus.Commit);

        order.OrderStatus = OrderStatus.Completed;

        await _work.CompleteAsync();

       
    }

    public async Task PaymentFailed(int paymentId, int orderId)
    {
        var payment = await _work.Get<PaymentDetail>().GetAsync(paymentId);
        
        if (payment is {PaymentStatus: PaymentStatus.Pending})
        {
            if (payment.PaymentStatus == PaymentStatus.Failed)
                return;
            payment.IsValid = false;
            payment.PaymentStatus = PaymentStatus.Failed;
        }

        var orderItems = await _work.Get<OrderItem>().Find(item => item.OrderId == orderId).ToListAsync();
        orderItems.ForEach(item =>
        {
            if (item.QrCode != null) item.QrCode.Status = QRCodeStatus.Active;
        });
        var order = await _work.Get<Order>().GetAsync(orderId);
        if (order != null) order.OrderStatus = OrderStatus.Failed;
        await _work.CompleteAsync();
        
    }

    public async Task<MomoResponse> ConfirmPaymentAsync(PaymentDetail payment)
    {
        var partnerCode = _momoSetting.PartnerCode;
        var orderId = payment.OrderId;
        var requestId = payment.RequestId;
        var amount = payment.TotalAmount;
        var requestType = "capture";
        var accessKey = _momoSetting.AccessKey;
        var secretKey = _momoSetting.SecretKey;
        var description = "";

        var param =  $"accessKey={accessKey}&amount={amount}&description={description}&orderId={orderId}&partnerCode={partnerCode}&requestId={payment.RequestId}&requestType={requestType}";
        var signature = MoMoSecurity.SignSha256(param, secretKey!);

        var url = "https://test-payment.momo.vn/v2/gateway/api/confirm";

        var confirmPaymentRequest = new JObject
        {
            {"partnerCode", partnerCode},
            {"requestId", requestId},
            {"orderId", orderId},
            {"requestType", requestType},
            {"amount", amount},
            {"lang", "en"},
            {"description", description},
            {"signature", signature}
        };

        var response = await PaymentRequest.SendConfirmPaymentRequest(url, confirmPaymentRequest.ToString());
        var momoResponse = JObject.Parse(response).ToObject<MomoResponse>();
        if (momoResponse == null)
            throw new CodingException("Can not connect to momo");

        momoResponse.Signature = signature;
        return momoResponse;
    }
}