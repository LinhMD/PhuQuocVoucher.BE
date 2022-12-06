using System.Globalization;
using System.Runtime.InteropServices;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Api.Ultility;
using PhuQuocVoucher.Business.Dtos.CartDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;
using System;
using System.Collections;
using System.Drawing;
using System.Net.Mail;
using IronBarCode;
using PhuQuocVoucher.Business.Dtos.MailDto;
using QRCoder;

namespace PhuQuocVoucher.Business.Services.Implements;

public class OrderService : ServiceCrud<Order>, IOrderService
{
    private ILogger<OrderService> _logger;

    private IOrderRepository _repository;
    private readonly IMailingService _mailingService;
    
    private readonly IVoucherService _voucherService;

    private IVoucherRepository _voucherRepository;
    public OrderService(IUnitOfWork work, ILogger<OrderService> logger, IMailingService mailingService, IVoucherService voucherService) : base(work.Get<Order>(), work, logger)
    {
        _logger = logger;
        _mailingService = mailingService;
        _voucherService = voucherService;
        _voucherRepository = (work.Get<Voucher>() as IVoucherRepository)!;
        _repository = (work.Get<Order>() as IOrderRepository)!;
    }

    public async Task<(IList<OrderView>, int)> GetOrdersByCustomerId(int cusId, PagingRequest request,
        OrderRequest<Order> sortBy)
    {
        var filter = Repository.Find(order => order.CustomerId == cusId);

        var total = filter.Count();
        var result = default(IList<OrderView>);
        try
        {
            result = await filter
                .OrderBy(sortBy)
                .ProjectToType<OrderView>()
                .Paging(request).ToListAsync();
        }
        catch (Exception e)
        {
            result = await filter
                .ProjectToType<OrderView>()
                .Paging(request).ToListAsync();
        }
       

        return (result, total);
    }
    

    public async Task<OrderView> PlaceOrderAsync(CartView cart, int cusId, int? sellerId = null)
    {
        //create order for the id
        var order = new Order()
        {
            CustomerId = cusId,
            TotalPrice = 0D,
            SellerId = sellerId,
            CreateAt = DateTime.Now
        };
        await UnitOfWork.Get<Order>().AddAsync(order);
        
        var total = 0L;
        if (cart == null)
            throw new ModelNotFoundException("Cart of customer not found");
        

        var comboIds = cart.CartItems.Where(v => v.IsCombo).Select(c => c.VoucherId).ToList();
        
        var comboDic = await UnitOfWork.Get<Voucher>().Find(cam => comboIds.Contains(cam.Id))
            .Select(c => new {
                c.Id, voucherInventorys =  
                c.Vouchers.Select(cam => cam.VoucherId).ToList()
            }).ToDictionaryAsync(c => c.Id, c => c.voucherInventorys);
        var combos = await UnitOfWork.Get<Voucher>().Find(cam => comboIds.Contains(cam.Id))
            .ToDictionaryAsync(c => c.Id, c => c);
        var vouchersIds = cart.CartItems.Where(v => !v.IsCombo).Select(c => c.VoucherId).ToList();

        vouchersIds.AddRange(comboDic.Values.Aggregate(new List<int>(), (vouch, comb) =>
        {
            vouch.AddRange(comb);
            return vouch;
        }));
        
        var uniqueVouchers = vouchersIds.Distinct().ToList();

        var qrcodeDic = (await UnitOfWork.Get<QrCode>()
            .Find(v => uniqueVouchers.Contains(v.VoucherId) && v.QrCodeStatus == QrCodeStatus.Active)
            .ToListAsync())
            .GroupBy(v => v.VoucherId)
            .ToDictionary(
                g => g.Key, 
                g => new Stack<QrCode>(g.ToList()));

        var orderDic = new Dictionary<int, List<QrCode>>();

        foreach (var item in cart.CartItems)
        {
            var vouchers = new List<QrCode>();
            if (item.IsCombo)
            {
                var combo = combos[item.VoucherId];
                --combo.Inventory;
                var voucherIds = comboDic[item.VoucherId];
                foreach (var voucherId in voucherIds)
                {
                    var voucherStack = qrcodeDic[voucherId];

                    for (var i = 0; i < item.Quantity; i++)
                    {
                        vouchers.Add(voucherStack.Pop());
                    }
                }
            }
            else
            {
                var voucherStack = qrcodeDic[item.VoucherId];
                for (var i = 0; i < item.Quantity; i++)
                {
                    vouchers.Add(voucherStack.Pop());
                }
            }
            orderDic.TryGetValue(item.VoucherId, out var values);
            if (values == null)
            {
                orderDic[item.VoucherId] = vouchers;
            }
            else
            {
                values.AddRange(vouchers);
                orderDic[item.VoucherId] = values;
            }
        }

        var orderVouchers = orderDic.Values.Aggregate(new List<QrCode>(), (list, vouchers) =>
        {
            list.AddRange(vouchers);
            return list;
        });

        order.QrCodes = orderVouchers;

        var vouchersId = order.QrCodes.Select(qr => qr.VoucherId).Distinct().ToList();

        var voucherPrice = await UnitOfWork
            .Get<Voucher>()
            .Find(v => vouchersId.Contains(v.Id))
            .Select(v => new {v.Id, v.SoldPrice})
            .ToDictionaryAsync(v => v.Id, v => v.SoldPrice);
        

        foreach (var orderVoucher in order.QrCodes)
        {
            orderVoucher.SoldPrice = voucherPrice[orderVoucher.VoucherId];
            orderVoucher.QrCodeStatus = QrCodeStatus.Pending;
            total += orderVoucher.SoldPrice;
        }

        order.TotalPrice = total;
        await UnitOfWork.CompleteAsync();
        await _voucherService.UpdateVoucherInventoryList(vouchersId);
        return order.Adapt<OrderView>();
    }

    
    public async Task<OrderView?> CancelOrderAsync(int id)
    {
        var order = await Repository.GetAsync(id);
        if (order == null) throw new ModelNotFoundException("Order Not Found With " + id);
        order.OrderStatus = OrderStatus.Canceled;
        var voucherIds = order.QrCodes.Select(qr => qr.VoucherId).Distinct().ToList();
        foreach (var qrCode in order.QrCodes)
        {
            qrCode.QrCodeStatus = QrCodeStatus.Active;
            qrCode.SoldPrice = 0;
            qrCode.Order = null;
            qrCode.OrderId = null;
        }
        order.QrCodes = new List<QrCode>();
        await Repository.CommitAsync();
        await _voucherService.UpdateVoucherInventoryList(voucherIds);
        var orderView = await Repository.Find<OrderView>(o => o.Id == id).FirstOrDefaultAsync() ?? throw new ModelNotFoundException("how did you get here?? ");
        return orderView!;
    }
    

    public byte[] GenerateQrCode(string hashCode)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(hashCode, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);
        // Styling a QR code and adding annotation text
        return qrCodeAsPngByteArr;
    }
    
    public async Task<(string email, List<Attachment>? attachments)> RenderOrderToHtml(Order order)
    {
        var filePath = Directory.GetCurrentDirectory() + $"\\MailTemplate\\OrderPrint.html";
        using var str = new StreamReader(filePath);
        var mailOrderPrint = await str.ReadToEndAsync();
        str.Close();
        mailOrderPrint = mailOrderPrint.Replace("[OrderId]", order.Id.ToString());
      
        filePath = Directory.GetCurrentDirectory() + $"\\MailTemplate\\OrderItem_QRCode.html";
        using var orderItemStream = new StreamReader(filePath);
        var htmlFormatOrderItem = await orderItemStream.ReadToEndAsync();

        var orderItemValues = string.Join("", order.QrCodes.Select(item => new Dictionary<string, string>()
        {
            {"ServiceName", item.Service.Name},
            {"HashCode", item.HashCode},
            {"VoucherId", item.Id.ToString()},
            {"ProviderName", item.Provider?.ProviderName ?? string.Empty},
            {"ProviderAddress", item.Provider?.Address ?? string.Empty },
            {"FromDate", item.Voucher.StartDate?.ToString("dd/MM/yyyy") ?? string.Empty},
            {"ToDate", item.Voucher.EndDate?.ToString("dd/MM/yyyy") ?? string.Empty},
            {"UseDate", item.UseDate?.ToString("dd/MM/yyyy") ?? string.Empty},
            {"Price", item.Voucher.SoldPrice.ToString(CultureInfo.InvariantCulture)}
        })
            .Select(item => item.Aggregate(htmlFormatOrderItem, 
                (current, dict) 
                    => current.Replace($"[{dict.Key}]", dict.Value))));
        var attachments = order.QrCodes.Select(o => 
            new Attachment(new MemoryStream(GenerateQrCode(o.HashCode)), o.HashCode, "image/png")
            {
                ContentId = o.HashCode
            }).ToList();
        return (mailOrderPrint.Replace("[OrderItemRow]", orderItemValues), attachments) ;
    }

    public async Task SendOrderEmailToCustomer(int orderId)
    {
        var order = await Repository.Find(o => o.Id == orderId).FirstOrDefaultAsync() ??
                    throw new ModelNotFoundException($"Order not found with Id {orderId}");
            
        var (email, attachments) = await RenderOrderToHtml(order);

        var customerEmail = await UnitOfWork.Get<Customer>().Find(cus => cus.Id == order.CustomerId).Select(c => c.UserInfo.Email).FirstOrDefaultAsync();
        var mailRequest = new MailRequest()
        {
            Body = email,
            ToEmail = customerEmail ?? "maidinhlinh967@gmail.com",
            Subject = "Your Order",
            Attachments = attachments
        };
        _mailingService.SendEmailAsync(mailRequest, true);
    }
}