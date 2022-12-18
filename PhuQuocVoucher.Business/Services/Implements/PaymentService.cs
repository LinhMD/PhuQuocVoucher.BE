﻿using System.Globalization;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PhuQuocVoucher.Api.Ultility;
using PhuQuocVoucher.Business.Dtos.MomoDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class PaymentService
{
    private readonly IUnitOfWork _work;
    private readonly MomoSetting _momoSetting;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IOrderService _orderService;

    public PaymentService(MomoSetting momoSetting, IUnitOfWork work, IBackgroundJobClient backgroundJobClient, IOrderService orderService)
    {
        _momoSetting = momoSetting;
        _work = work;
        _backgroundJobClient = backgroundJobClient;
        _orderService = orderService;
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
        var ipnUrl = _momoSetting.IpnCallback;
        var requestType = "captureWallet";


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
            RequestId = requestId,
            Order = order,
        };

        await _work.Get<PaymentDetail>().AddAsync(payment);

        var rawHash = $"accessKey={_momoSetting.AccessKey}&amount={order.TotalPrice}&extraData={extraData}&ipnUrl={_momoSetting.IpnCallback}&orderId={payment.Id + "_s"}&orderInfo={orderInfo}&partnerCode={_momoSetting.PartnerCode}&redirectUrl={redirectUrl}&requestId={requestId}&requestType={requestType}";

        var signature = MoMoSecurity.SignSha256(rawHash, secretKey!);
        
        signature.Dump();
        
        var getPaymentMethodRequest = new JObject
        {
            {"partnerCode", partnerCode},
            {"partnerName", "Phu Quoc Company"},
            {"storeId", "Phu QuocVoucher"},
            {"requestId", requestId},
            {"amount", order.TotalPrice},
            {"orderId", payment.Id + "_s"}, //cheating
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
        //_backgroundJobClient.Schedule(() => PaymentFailed(payment.Id, orderId), TimeSpan.FromMinutes(15));
        return momoResponse;
    }

    public async Task UpdateStatusWhenSuccessAsync(MomoIPNRequest request)
    {


        
        var paymentId = int.Parse(request.OrderId.Split("_")[0]);
        var order = await _work.Get<Order>().IncludeAll().Where(order => order.PaymentDetailId == paymentId).FirstOrDefaultAsync();

        if (request.ResultCode != 9000 && request.ResultCode != 0)
        {
            if (order != null) 
                await PaymentFailed(paymentId, order.Id);
            return;
        }
        
        if (order == null)
            throw new ModelNotFoundException($"Order not found with id {request.OrderId}");
        
        var paymentDetail = await _work.Get<PaymentDetail>().Find(p => p.RequestId.ToString() == request.RequestId)
            .FirstOrDefaultAsync();
        
        if (paymentDetail == null || paymentDetail.OrderId != order.Id)
            throw new ModelValueInvalidException("");

        paymentDetail.PaymentStatus = PaymentStatus.Success;

        var qrCodes = await _work.Get<QrCode>().Find(qr => qr.OrderId == order.Id).ToListAsync();
        foreach (var orderQrCode in qrCodes)
        {
            orderQrCode.QrCodeStatus = QrCodeStatus.Commit;
        }
        order.OrderStatus = OrderStatus.Completed;
        order.CompleteDate = DateTime.Now;
        
        var seller = await _work.Get<Seller>().Find(s => s.Id == order.SellerId).FirstOrDefaultAsync();

        if (seller != null)
        {
            seller.Exp += (int) (order.TotalPrice / 10000);
            var rank = await _work.Get<SellerRank>()
                .Find(r => r.EpxRequired <= seller.Exp)
                .OrderByDescending(r => r.EpxRequired)
                .FirstOrDefaultAsync();
            
            if (rank != null && seller.RankId != rank.Id)
            {
                seller.Rank = rank;
                seller.RankId = rank.Id;
                seller.CommissionRate = rank.CommissionRatePercent;
            }
        }
        
        await _work.CompleteAsync();
        if(order.SellerId == null)
            await _orderService.SendOrderEmailToCustomer(order.Id);
        
        var momoResponse = await ConfirmPaymentAsync(paymentDetail);
        
        
        
        /*if (signature != momoResponse.Signature)
            throw new ModelValueInvalidException("Signature is not valid");*/

        /*if (momoResponse.resultCode is not 9000 or 0)
            throw new ModelValueInvalidException("Failed momo payment");
            */

        /*if (Math.Abs(momoResponse.amount - paymentDetail!.TotalAmount ?? 0) > 0.1)
            throw new ModelValueInvalidException("Amount is not valid");*/
        
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
        var order = await _work.Get<Order>().Find(order => order.PaymentDetailId == paymentId).FirstOrDefaultAsync();

        if (order == null)
            throw new ModelNotFoundException($"Order not found with id {orderId}");

        var qrCodes = await _work.Get<QrCode>().Find(qr => qr.OrderId == order.Id).ToListAsync();
        foreach (var qrCode in qrCodes)
        {
            qrCode.QrCodeStatus = QrCodeStatus.Active;
            qrCode.OrderItemId = null;
            qrCode.OrderId = null;
            qrCode.CommissionFee = 0;
            qrCode.SellerCommissionFee = 0;
            qrCode.ProviderRevenue = 0;
            qrCode.SoldDate = null;
        }

        order.OrderStatus = OrderStatus.Failed;
        await _work.CompleteAsync();
        
    }

    public async Task<MomoConfirmResponse> ConfirmPaymentAsync(PaymentDetail payment)
    {
        var partnerCode = _momoSetting.PartnerCode;
        var orderId = payment.Id + "_s";
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
        var momoResponse = JObject.Parse(response).ToObject<MomoConfirmResponse>();
        if (momoResponse == null)
            throw new CodingException("Can not connect to momo");

        return momoResponse;
    }
}