using System.Globalization;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.CartDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;
using System.Net.Mail;
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
            CreateAt = DateTime.Now,
            
        };
        await UnitOfWork.Get<Order>().AddAsync(order);
        
        var total = 0L;
        if (cart == null)
            throw new ModelNotFoundException("Cart of customer not found");

        var seller = await UnitOfWork.Get<Seller>().Find(s => s.Id == sellerId).Select(s => new{s.Rank.CommissionRatePercent}).FirstOrDefaultAsync();

        var vouchersIds = cart.CartItems.Where(v => !v.IsCombo).Select(c => c.VoucherId).ToList();
        
        var comboIds = cart.CartItems.Where(v => v.IsCombo).Select(c => c.VoucherId).ToList();
        
        var providerDic = await UnitOfWork.Get<Voucher>()
            .Find(v => cart.CartItems.Select(i => i.VoucherId).Contains(v.Id)).Select(v => new{v.Id, v.ProviderId})
            .ToDictionaryAsync( v => v.Id, v => v.ProviderId);
        
        var comboDic = await UnitOfWork.Get<Voucher>().Find(cam => comboIds.Contains(cam.Id))
            .Select(c => new {
                c.Id, 
                voucherInventorys = c.Vouchers.Select(cam => cam.VoucherId).ToList()
            }).ToDictionaryAsync(c => c.Id, c => c.voucherInventorys);
        var combos = await UnitOfWork.Get<Voucher>().Find(cam => comboIds.Contains(cam.Id))
            .ToDictionaryAsync(c => c.Id, c => c);
        vouchersIds.AddRange(comboIds);
        
        vouchersIds.AddRange(comboDic.Values.Aggregate(new List<int>(), (vouch, comb) =>
        {
            vouch.AddRange(comb);
            return vouch;
        }));
        
        var uniqueVouchers = vouchersIds.Distinct().ToList();
        vouchersIds = vouchersIds.Distinct().ToList();
        var qrcodeDic = (await UnitOfWork.Get<QrCode>()
            .Find(v => uniqueVouchers.Contains(v.VoucherId) && v.QrCodeStatus == QrCodeStatus.Active)
            .ToListAsync())
            .GroupBy(v => v.VoucherId)
            .ToDictionary(
                g => g.Key, 
                g => new Stack<QrCode>(g.ToList()));

        var voucherDic = await UnitOfWork
            .Get<Voucher>()
            .Find(v => vouchersIds.Contains(v.Id))
            .Select(v => new {v.Id, v.SoldPrice, v.CommissionRate})
            .ToDictionaryAsync(v => v.Id, v => v);

        var orderItems = new List<OrderItem>();
        foreach (var item in cart.CartItems)
        {
            providerDic.TryGetValue(item.VoucherId, out var proId);
            orderItems.Add(new OrderItem()
            {
                VoucherId = item.VoucherId,
                Quantity = item.Quantity,
                OrderId = order.Id,
                SoldPrice = voucherDic[item.VoucherId].SoldPrice,
                ProviderId = proId,
                SellerId = sellerId,
                IsCombo = item.IsCombo,
                QrCodes = new List<QrCode>(),
                CreateAt = DateTime.Now,
                
            });
        }

        await UnitOfWork.Get<OrderItem>().AddAllAsync(orderItems);
        orderItems = await UnitOfWork.Get<OrderItem>().Find(item => item.OrderId == order.Id).ToListAsync();
        
        var qrCodes = new List<QrCode>();
        foreach (var item in orderItems)
        {
            
            if (item.IsCombo)
            {
                var combo = combos[item.VoucherId];
                var sellerCommission = 0L;
                var providerRevenue = 0L;
                var voucherIds = comboDic[item.VoucherId];
                foreach (var voucherId in voucherIds)
                {
                    var qrCodeStack = qrcodeDic[voucherId];

                    for (var i = 0; i < item.Quantity; i++)
                    {
                        var qrCode = qrCodeStack.Pop();
                        
                        qrCode.OrderItemId = item.Id;
                        qrCode.OrderId = order.Id;
                        var commissionFee = (long) (voucherDic[voucherId].SoldPrice *
                                                    voucherDic[voucherId].CommissionRate);
                        if (seller != null)
                        {
                            qrCode.SellerCommissionFee = (long) (seller.CommissionRatePercent * commissionFee);
                            sellerCommission += qrCode.SellerCommissionFee;
                        }

                        qrCode.ProviderRevenue = voucherDic[voucherId].SoldPrice - commissionFee;
                        providerRevenue += qrCode.ProviderRevenue;
                        qrCode.CommissionFee = commissionFee - qrCode.SellerCommissionFee;
                        item.QrCodes.Add(qrCode);
                        qrCodes.Add(qrCode);
                    }
                }

                item.SellerCommission = sellerCommission;
                item.ProviderRevenue = providerRevenue;
                item.CommissionFee = voucherDic[item.VoucherId].SoldPrice * item.Quantity - item.QrCodes.Select(qr => qr.ProviderRevenue + qr.SellerCommissionFee).Sum();
            }
            else
            {
                var qrCodeStack = qrcodeDic[item.VoucherId];
                var sellerCommission = 0L;
                var providerRevenue = 0L;
                for (var i = 0; i < item.Quantity; i++)
                {
                    var qrCode = qrCodeStack.Pop();
                    qrCode.OrderItemId = item.Id;
                    qrCode.OrderId = order.Id;
                    var commissionFee = (long) (voucherDic[item.VoucherId].SoldPrice *
                                                   voucherDic[item.VoucherId].CommissionRate);
                    if (seller != null)
                    {
                        qrCode.SellerCommissionFee = (long) (seller.CommissionRatePercent * commissionFee);
                        sellerCommission += qrCode.SellerCommissionFee;
                    }

                    qrCode.ProviderRevenue = voucherDic[item.VoucherId].SoldPrice - commissionFee;
                    providerRevenue += qrCode.ProviderRevenue;
                    qrCode.CommissionFee = commissionFee - qrCode.SellerCommissionFee;
                    item.QrCodes.Add(qrCode);
                    qrCodes.Add(qrCode);
                }
                item.SellerCommission = sellerCommission;
                item.ProviderRevenue = providerRevenue;
                item.CommissionFee = item.QrCodes.Select(qr => qr.CommissionFee).Sum();
            }
        }

        foreach (var item in orderItems)
        {
            item.SoldPrice = voucherDic[item.VoucherId].SoldPrice;
            
            total += item.SoldPrice * item.Quantity;
        }
        
        foreach (var qrCode in qrCodes)
        {
            qrCode.QrCodeStatus = QrCodeStatus.Pending;
            qrCode.SoldDate = DateTime.Today;
        }
        
        order.TotalPrice = total;
        await UnitOfWork.CompleteAsync();
        var comboQuantity = order.OrderItems.Where(o => o.IsCombo).Select(o => new {o.VoucherId, o.Quantity})
            .GroupBy(o => o.VoucherId)
            .ToDictionary(o => o.Key, o => o.Select(i => i.Quantity).Sum());
        await _voucherService.UpdateVoucherInventoryList(vouchersIds, comboQuantity);
        return order.Adapt<OrderView>();
    }

    
    public async Task<OrderView?> CancelOrderAsync(int id)
    {
        var order = await Repository.GetAsync(id);
        if (order == null) throw new ModelNotFoundException("Order Not Found With " + id);
        order.OrderStatus = OrderStatus.Canceled;
        var qrCodes = order.OrderItems.Select(item => item.QrCodes).Aggregate(new List<QrCode>(), (accum, codes) =>
        {
            accum.AddRange(codes);
            return accum;
        });
        var voucherIds = qrCodes.Select(qr => qr.VoucherId).Distinct().ToList();
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
        var qrCodes = await UnitOfWork.Get<QrCode>().Find(code => code.OrderId == order.Id).Select(item => new
        {
            ServiceName = item.Service.Name,
            item.HashCode,
            item.Id,
            item.Provider!.ProviderName,
            item.Provider!.Address,
            item.Voucher.StartDate,
            item.Voucher.EndDate,
            item.Voucher.SoldPrice,
            item.Voucher.VoucherValue
        }).ToListAsync();
        
        var orderItemValues = string.Join("", qrCodes.Select(item => new Dictionary<string, string>()
        {
            {"ServiceName", item.ServiceName},
            {"QRCodeId", item.Id.ToString()},
            {"HashCode", item.HashCode},
            {"ProviderName", item.ProviderName ?? string.Empty},
            {"ProviderAddress", item.Address ?? string.Empty },
            {"FromDate", item.StartDate?.ToString("dd/MM/yyyy") ?? string.Empty},
            {"ToDate", item.EndDate?.ToString("dd/MM/yyyy") ?? string.Empty},
            {"Value", item.VoucherValue.ToString()},
            {"Price", item.SoldPrice.ToString(CultureInfo.InvariantCulture)}
        })
            .Select(item => item.Aggregate(htmlFormatOrderItem, 
                (current, dict) 
                    => current.Replace($"[{dict.Key}]", dict.Value))));
        var attachments = qrCodes.Select(o => 
            new Attachment(new MemoryStream(GenerateQrCode(o.HashCode)), o.HashCode, "image/png")
            {
                ContentId = o.HashCode
            }).ToList();
        return (mailOrderPrint.Replace("[OrderItemRow]", orderItemValues), attachments) ;
    }

    public async Task<string> SendOrderEmailToCustomer(int orderId)
    {
        var order = await Repository.Find(o => o.Id == orderId).FirstOrDefaultAsync() ??
                    throw new ModelNotFoundException($"Order not found with Id {orderId}");
            
        var (email, attachments) = await RenderOrderToHtml(order);

        var customerEmail = await UnitOfWork.Get<Customer>().Find(cus => cus.Id == order.CustomerId).Select(c => c.UserInfo!.Email).FirstOrDefaultAsync();
        var mailRequest = new MailRequest()
        {
            Body = email,
            ToEmail = customerEmail ?? "maidinhlinh967@gmail.com",
            Subject = "Your Order",
            Attachments = attachments
        };
        _mailingService.SendEmailAsync(mailRequest, true);

        return email;
    }
}