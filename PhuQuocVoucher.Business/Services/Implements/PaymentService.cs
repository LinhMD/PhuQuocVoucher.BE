using System.Globalization;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using Hangfire;
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

        if (order.OrderStatus == OrderStatus.Completed ||
            order.OrderStatus == OrderStatus.Used ||
            order.OrderStatus == OrderStatus.Canceled)
            throw new ModelValueInvalidException("Can not create payment request for order " + orderId);
        var endpoint = _momoSetting.EndPoint;
        var partnerCode = _momoSetting.PartnerCode;
        var accessKey = _momoSetting.AccessKey;
        var secretKey = _momoSetting.SecretKey;
        var orderInfo = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + order.Id;
        var redirectUrl = isMobile ? "https://phuquocvoucher.page.link/TG78" : "https://phuquocvoucher.page.link/TG78";
        var ipnUrl = "https://webapp-221010174451.azurewebsites.net/api/v1/momo/callback";
        var requestType = "captureWallet";


        var amount = order.TotalPrice.ToString(CultureInfo.InvariantCulture);
        var requestId = Guid.NewGuid();
        var extraData = "";

        var rawHash = "accessKey=" + accessKey + "&amount=" + amount + "&extraData=" + extraData + "&ipnUrl=" + ipnUrl +
                      "&orderId=" + orderId + "&orderInfo=" + orderInfo + "&partnerCode=" + partnerCode +
                      "&redirectUrl=" + redirectUrl + "&requestId=" + requestId + "&requestType=" + requestType;

        var signature = MoMoSecurity.SignSha256(rawHash, secretKey!);

        var message = new JObject
        {
            {"partnerCode", partnerCode},
            {"partnerName", "Phu Quoc Company"},
            {"storeId", "Phu QuocVoucher"},
            {"requestId", requestId},
            {"amount", amount},
            {"orderId", orderId},
            {"orderInfo", orderInfo},
            {"redirectUrl", redirectUrl},
            {"ipnUrl", ipnUrl},
            {"lang", "en"},
            {"autoCapture", false},
            {"extraData", extraData},
            {"requestType", requestType},
            {"signature", signature}
        };

        var response = await PaymentRequest.SendPaymentRequestAsync(endpoint!, message.ToString());
        var jMessage = JObject.Parse(response);
        Console.WriteLine(jMessage.ToString());
        var momoResponse = jMessage.ToObject<MomoResponse>();
        if (momoResponse == null)
            throw new ModelValueInvalidException("Can not create a payment request");

        order.PaymentRequestId = requestId;
        order.OrderStatus = OrderStatus.Processing;
        var payment = new PaymentDetail()
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
        await _work.CompleteAsync();
        _backgroundJobClient.Schedule(() => PaymentFailed(payment.Id), TimeSpan.FromMinutes(15));
        return momoResponse;
    }

    public async Task UpdateStatusWhenSuccessAsync(MomoIPNRequest request)
    {
        if (request.ResultCode != 9000)
            throw new ModelValueInvalidException("Something went wrong when processing payment");

        var accessKey = _momoSetting.AccessKey;
        var secretKey = _momoSetting.SecretKey;
        var description = "";
        var requestType = "capture";

        var orderId = request.OrderId;
        var order = await _work.Get<Order>().GetAsync(orderId);

        if (order == null)
            throw new ModelNotFoundException($"Order not found with id {request.OrderId}");

        var paymentDetail = await _work.Get<PaymentDetail>().Find(p => p.RequestId.ToString() == request.RequestId)
            .FirstOrDefaultAsync();

        if (paymentDetail == null || paymentDetail.OrderId != orderId)
            throw new ModelValueInvalidException("");

        var momoResponse = await ConfirmPaymentAsync(request);
        var rawHash = "accessKey=" + accessKey + "&amount=" + request.Amount + "&description=" + description +
                      "&orderId=" + request.OrderId + "&partnerCode=" + request.PartnerCode + "&requestId=" +
                      request.RequestId + "&requestType=" + requestType;

        var signature = MoMoSecurity.SignSha256(rawHash, secretKey!);

        if (signature != momoResponse.Signature)
            throw new ModelValueInvalidException("Signature is not valid");

        if (momoResponse.resultCode != 0)
            throw new ModelValueInvalidException("Failed when update status");

        if (Math.Abs(momoResponse.amount - paymentDetail!.TotalAmount ?? 0) > 0.1)
            throw new ModelValueInvalidException("Amount is not valid");

        paymentDetail.PaymentStatus = PaymentStatus.Success;

        var qrCodeIds = await _work.Get<OrderItem>().Find(item => item.OrderId == order.Id && item.QrCodeId != null)
            .Select(item => item.QrCodeId).ToListAsync();

        var qrCodeInfos = await _work.Get<QrCodeInfo>().Find(qr => qrCodeIds.Contains(qr.Id)).ToListAsync();
        qrCodeInfos.ForEach(qr => qr.Status = QRCodeStatus.Commit);

        order.OrderStatus = OrderStatus.Completed;

        await _work.CompleteAsync();

        /*entity.Status = PaymentStatus.Success.ToString();
        await _paymentRepository.Update(entity);

        //send notification to client
        var questName = _questRepository.Get(entity.QuestId).Result.Title;

        // send notification to client
        await _notificationService.CreateAsync(new NotifyUserRequestModel
        {
            Content = "New payment has been made successfully " + entity.TotalAmount + " VND" +
                      " for quest " + ConvertLanguage(Language.vi, questName!),
            CreatedDate = CurrentDateTime(),
            PaymentId = entity.Id,
            //UserId = customerId,
        });

        //send mail to customer when payment success
        var customerEmail = _userManager!.FindByIdAsync(entity.CustomerId).Result.Email;

        /*var MyQRWithLogo = QRCodeWriter.CreateQrCodeWithLogo("https://ironsoftware.com/csharp/barcode/", "visual-studio-logo.png", 500);
        MyQRWithLogo.ChangeBarCodeColor(System.Drawing.Color.DarkGreen);

        var barcode = MyQRWithLogo.BinaryValue;#1#

        var message = "<h1>Payment Success</h1>" + "<h3>Dear " + customerEmail + "</h3>" +
                      "<p>Your payment has been succeeded</p>" + "<p>Your order is: " + entity.Id + "</p>" +
                      "<p>Your quest name is: " + ConvertLanguage(Language.vi, questName!) + "/ " +
                      ConvertLanguage(Language.en, questName!) + "</p>" + "<p>Quantity is: " + entity.Quantity +
                      "</p>" + "<p>Your order total amount is: " + entity.TotalAmount + "</p>" +
                      //"src=\"data:image/png;base64," + Convert.ToBase64String(barcode) + "\"" +
                      "<p>Your playing code is: " + entity.Id + "</p>" + "<p>Your order ticket will be invalid at " +
                      entity.CreatedDate.AddDays(2).ToString("dd/MM/yyyy") + "</p>" +
                      "<p>Thank you for using our service</p>";

        _backgroundJobClient.Enqueue( () =>
            _emailSender.SendMailConfirmAsync(customerEmail, "Payment Information", message));*/
    }

    public async Task PaymentFailed(int paymentId)
    {
        var payment = await _work.Get<PaymentDetail>().GetAsync(paymentId);
        if (payment is {PaymentStatus: PaymentStatus.Pending})
        {
            payment.IsValid = false;
            payment.PaymentStatus = PaymentStatus.Failed;
            await _work.CompleteAsync();
        }
    }

    public async Task<MomoResponse> ConfirmPaymentAsync(MomoIPNRequest request)
    {
        var partnerCode = request.PartnerCode;
        var orderId = request.OrderId;
        var requestId = request.RequestId;
        var amount = request.Amount;
        var requestType = "capture";
        var accessKey = _momoSetting.AccessKey;
        var secretKey = _momoSetting.SecretKey;
        var description = "";

        var param = "accessKey=" + accessKey + "&amount=" + amount + "&description=" + description + "&orderId=" +
                    orderId + "&partnerCode=" + partnerCode + "&requestId=" + requestId + "&requestType=" + requestType;

        var signature = MoMoSecurity.SignSha256(param, secretKey!);

        var url = "https://test-payment.momo.vn/v2/gateway/api/confirm";

        var message = new JObject
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

        var response = await PaymentRequest.SendConfirmPaymentRequest(url, message.ToString());
        var momoResponse = JObject.Parse(response).ToObject<MomoResponse>();
        if (momoResponse == null)
            throw new CodingException("Can not connect to momo");

        momoResponse.Signature = signature;
        return momoResponse;
    }
}