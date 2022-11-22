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
using PhuQuocVoucher.Business.Dtos.OrderItemDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;
using System;
using PhuQuocVoucher.Business.Dtos.MailDto;

namespace PhuQuocVoucher.Business.Services.Implements;

public class OrderService : ServiceCrud<Order>, IOrderService
{
    private ILogger<OrderService> _logger;

    private IOrderRepository _repository;
    private readonly IMailingService _mailingService;
    
    private readonly IVoucherService _voucherService;
    public OrderService(IUnitOfWork work, ILogger<OrderService> logger, IMailingService mailingService, IVoucherService voucherService) : base(work.Get<Order>(), work, logger)
    {
        _logger = logger;
        _mailingService = mailingService;
        _voucherService = voucherService;
        _repository = work.Get<Order>() as IOrderRepository;
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


    /*private async Task ValidateInventoryEnoughAsync(IEnumerable<OrderItem> orderItems)
    {
        var itemCount = orderItems.GroupBy(o => o.OrderProductId);
        //check inventory
        foreach (var items in itemCount)
        {
            var product = await UnitOfWork.Get<Product>()
                .Find(product => product.Id == items.Key && product.Inventory >= items.Count()).FirstOrDefaultAsync();

            if (product == null)
            {
                throw new ModelValueInvalidException(
                    $"Voucher with product id {items.ToList().First().OrderProductId} do not have enough inventory");
            }
            product.Inventory -= items.Count();
        }
        await UnitOfWork.CompleteAsync();
    }*/

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
        
        double total = 0;
        
        var sellerRate = await UnitOfWork.Get<Seller>().Find(seller => seller.Id == sellerId).Select(s => s.CommissionRate).FirstOrDefaultAsync();
        //create order item
        var orderItems = new List<OrderItem>();
        var priceIds = cart.CartItems.Select(o => o.PriceId).ToList();
        var priceBooks = await UnitOfWork.Get<PriceBook>().Find(pb => priceIds.Contains(pb.Id)).ToListAsync();
        var priceBooksDic = priceBooks.ToDictionary(book => book.Id, book => (book.Price, book.PriceLevel));
        var voucherIds = priceBooks.Select(p => p.VoucherId).Distinct();
        var vouchers = await UnitOfWork.Get<Voucher>().Find(v => voucherIds.Contains(v.Id))
            .ToDictionaryAsync(v => v.Id, v => v);
        
        var serviceIds = vouchers.Values.Select(v => v.ServiceId).ToList();
        var serviceRates = await UnitOfWork.Get<Service>()
            .Find(s => serviceIds.Contains(s.Id))
            .ToDictionaryAsync(s => s.Id, s => s.CommissionRate);
        var qrCodes = (await UnitOfWork.Get<QrCodeInfo>().Find(qr => voucherIds.Contains(qr.VoucherId) && qr.Status == QRCodeStatus.Active).ToListAsync())
            .GroupBy(qr => qr.VoucherId)
            .ToDictionary(
                qrs => qrs.Key,
                qrs => new Stack<QrCodeInfo>(qrs.ToList()));
        var voucherQuantities = cart.CartItems
            .GroupBy(i => i.VoucherId)
            .ToDictionary(
                group => group.Key, 
                group => group.ToList().Select(item => item.Quantity).Sum());
        var idsOutOfQrCode = voucherQuantities.Keys.Except(qrCodes.Keys).ToList();
        
        if (idsOutOfQrCode.Any())
        {
            throw new ModelValueInvalidException($"Voucher ids out of QrCode : {string.Join(",", idsOutOfQrCode)}");
        }

        var voucherIdsNotHaveEnoughQrCode = qrCodes.Keys.Intersect(voucherQuantities.Keys).Where(voucherId => voucherQuantities[voucherId] > qrCodes[voucherId].Count).ToList();
        if (voucherIdsNotHaveEnoughQrCode.Any())
        {
            var vouchersNotHaveEnough = voucherIdsNotHaveEnoughQrCode
                .Select(id => $"{id} - quantity: {voucherQuantities[id]}").ToList();
            throw new ModelValueInvalidException($"Voucher ids out of QrCode : {string.Join(",", vouchersNotHaveEnough)}");
        }
        
        foreach (var items in cart!.CartItems)
        {
            for (var i = 0; i < items.Quantity; i++)
            {
                var qrCode = qrCodes[items.VoucherId].Pop();
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    PriceId = items.PriceId,
                    SoldPrice = priceBooksDic[items.PriceId].Price,
                    ProviderId = vouchers[items.VoucherId].ProviderId,
                    CreateAt = DateTime.Now,
                    SellerId = sellerId,
                    Status = ModelStatus.Active,
                    QrCodeId =  qrCode.Id,
                    QrCode = qrCode,
                    VoucherId = items.VoucherId,
                    SellerRate = sellerRate * priceBooksDic[items.PriceId].Price,
                    ProviderRate = serviceRates[vouchers[items.VoucherId].ProviderId] * priceBooksDic[items.PriceId].Price,
                    UseDate = items.UseDate,
                    ProfileId = items.ProfileId,
                    CustomerId = order.CustomerId,
                    PriceLevel = priceBooksDic[items.PriceId].PriceLevel
                };
                orderItem.Validate();
                qrCode.Status = QRCodeStatus.Pending;
                orderItems.Add(orderItem);
                total += items.Price;
            }
        }

        order.TotalPrice = total;
        
        /*await ValidateInventoryEnoughAsync(orderItems);*/
        await UnitOfWork.Get<OrderItem>().AddAllAsync(orderItems);
        await UnitOfWork.Get<CartItem>().RemoveAllAsync(cart.CartItems.Select(c => new CartItem {Id = c.Id}));
        order.OrderItems = orderItems;
        await _voucherService.UpdateInventory();
        order.Validate();
        
        
        return order.Adapt<OrderView>();
    }

    
    public async Task<OrderView> CreateOrderAsync(CreateOrder createOrder, int? sellerId = null)
    {
        try
        {
            //create order for the id
            var order = new Order()
            {
                CustomerId = createOrder.CustomerId,
                TotalPrice = 0D,
                SellerId = createOrder.SellerId,
                CreateAt = DateTime.Now
            };
            order.Validate();
            await UnitOfWork.Get<Order>().AddAsync(order);
            var sellerRate = await UnitOfWork.Get<Seller>().Find(seller => seller.Id == sellerId).Select(s => s.CommissionRate).FirstOrDefaultAsync();
            
            foreach (var item in createOrder.OrderItems)
            {
                item.OrderId = order.Id;
            }
            
            /*var orderItems = createOrder.OrderItems
                .Select(o => (o as ICreateRequest<OrderItem>).CreateNew(UnitOfWork)).ToList();*/
            var priceIds = createOrder.OrderItems.Select(o => o.PriceId).ToList();

            var priceBooks = await UnitOfWork.Get<PriceBook>().Find(pb => priceIds.Contains(pb.Id)).ToListAsync();
            var priceBooksDic = priceBooks.ToDictionary(book => book.Id, book => (book.Price, book.PriceLevel));
            var voucherIds = priceBooks.Select(p => p.VoucherId).Distinct();
            
            var vouchers = await UnitOfWork.Get<Voucher>().Find(v => voucherIds.Contains(v.Id))
                .ToDictionaryAsync(v => v.Id, v => v);
            
            var serviceIds = vouchers.Values.Select(v => v.ServiceId).ToList();
            var serviceRates = await UnitOfWork.Get<Service>()
                .Find(s => serviceIds.Contains(s.Id))
                .ToDictionaryAsync(s => s.Id, s => s.CommissionRate);
            


            var qrCodes = (await UnitOfWork.Get<QrCodeInfo>().Find(qr => voucherIds.Contains(qr.VoucherId)).ToListAsync())
                .GroupBy(qr => qr.VoucherId)
                .ToDictionary(
                    qrs => qrs.Key,
                    qrs => new Stack<QrCodeInfo>(qrs.ToList()));
            var voucherQuantities = createOrder.OrderItems
                .GroupBy(i => i.VoucherId)
                .ToDictionary(
                    group => group.Key, 
                    group => group.ToList().Count);
            
            var idsOutOfQrCode = voucherQuantities.Keys.Except(qrCodes.Keys).ToList();
            if (idsOutOfQrCode.Any())
            {
                throw new ModelValueInvalidException($"Voucher ids out of QrCode : {string.Join(",", idsOutOfQrCode)}");
            }

            var voucherIdsNotHaveEnoughQrCode = qrCodes.Keys.Intersect(voucherQuantities.Keys)
                .Where(voucherId => voucherQuantities[voucherId] > qrCodes[voucherId].Count).ToList();
            if (voucherIdsNotHaveEnoughQrCode.Any())
            {
                throw new ModelValueInvalidException($"Voucher ids out of QrCode : {string.Join(",", voucherIdsNotHaveEnoughQrCode)}");
            }
            
            var orderItems = createOrder.OrderItems
                .Select(o => new OrderItem
                {
                    OrderId = order.Id,
                    PriceId = o.PriceId,
                    SoldPrice = priceBooksDic[o.PriceId].Price,
                    ProviderId = vouchers[o.VoucherId].ProviderId,
                    CreateAt = DateTime.Now,
                    SellerId = sellerId,
                    Status = ModelStatus.Active,
                    QrCode = qrCodes[o.VoucherId].Pop(),
                    VoucherId = o.VoucherId,
                    SellerRate = sellerRate * priceBooksDic[o.PriceId].Price,
                    ProviderRate = serviceRates[vouchers[o.VoucherId].ProviderId] * priceBooksDic[o.PriceId].Price,
                    UseDate = o.UseDate,
                    ProfileId = o.ProfileId,
                    CustomerId = order.CustomerId,
                    PriceLevel = priceBooksDic[o.PriceId].PriceLevel
                }).Peek(o =>
                {
                    o.QrCodeId = o.QrCode!.Id;
                    o.QrCode.Status = QRCodeStatus.Pending;
                }).ToList();
            
            
            foreach (var item in orderItems)
            {
                try
                {
                    item.Validate();
                }
                catch (Exception e)
                {
                    e.StackTrace.Dump();
                }
            }

            /*await ValidateInventoryEnoughAsync(orderItems)*/;

            await UnitOfWork.Get<OrderItem>().AddAllAsync(orderItems);

            order.TotalPrice = await UnitOfWork.Get<OrderItem>().Find(item => item.OrderId == order.Id)
                .Select(item => item.Price.Price).SumAsync();

            order.OrderItems = orderItems;
            await _voucherService.UpdateInventory();
            await UnitOfWork.CompleteAsync();
            return order.Adapt<OrderView>();
        }
        catch (DbUpdateException e)
        {
            e.InnerException?.StackTrace.Dump();
            throw new DbQueryException(e.InnerException?.Message!, DbError.Create);
        }
    }

    public async Task<OrderView?> CancelOrderAsync(int id)
    {
        var order = await Repository.GetAsync(id);
        if (order == null) throw new ModelNotFoundException("Order Not Found With " + id);
        order.OrderStatus = OrderStatus.Canceled;

        await Repository.CommitAsync();
        await _voucherService.UpdateInventory();
        var orderView = await Repository.Find<OrderView>(o => o.Id == id).FirstOrDefaultAsync() ?? throw new ModelNotFoundException("how did you get here?? ");
        return orderView!;
    }

    public async Task<string> RenderOrderToHtml(Order order)
    {
        
        var filePath = Directory.GetCurrentDirectory() + $"\\MailTemplate\\OrderPrint.html";
        using var str = new StreamReader(filePath);
        var mailOrderPrint = await str.ReadToEndAsync();
        str.Close();
        mailOrderPrint = mailOrderPrint.Replace("[OrderId]", order.Id.ToString());
      
        filePath = Directory.GetCurrentDirectory() + $"\\MailTemplate\\OrderItem_QRCode.html";
        using var orderItemStream = new StreamReader(filePath);
        var htmlFormatOrderItem = await orderItemStream.ReadToEndAsync();

        var orderItemValues = string.Join("", order.OrderItems.Select(item => new Dictionary<string, string>()
        {
            {"VoucherName", item.Voucher.VoucherName ?? string.Empty},
            {"QrCodeImgLink", item.QrCode?.ImgLink ?? string.Empty},
            {"QRCodeId", item.QrCode?.Id.ToString() ?? string.Empty},
            {"ProviderName", item.Provider.ProviderName ?? string.Empty},
            {"ProviderAddress", item.Provider.Address ?? string.Empty },
            {"FromDate", item.Voucher.StartDate?.ToString("dd/MM/yyyy") ?? string.Empty},
            {"ToDate", item.Voucher.EndDate?.ToString("dd/MM/yyyy") ?? string.Empty},
            {"UseDate", item.UseDate?.ToString("dd/MM/yyyy") ?? string.Empty},
            {"CustomerName", (item.Profile?.Name ?? order.Customer?.CustomerName) ?? string.Empty },
            {"CivilInfo", item.Profile?.CivilIdentify ?? string.Empty},
            {"PriceLevel", item.PriceLevel.ToString() ?? PriceLevel.Default.ToString()},
            {"Price", item.SoldPrice.ToString(CultureInfo.InvariantCulture)}
        }).Select(item => item.Aggregate(htmlFormatOrderItem, 
                (current, dict) 
                    => current.Replace($"[{dict.Key}]", dict.Value))));

        return mailOrderPrint.Replace("[OrderItemRow]", orderItemValues);
    }

    public async Task SendOrderEmailToCustomer(int orderId)
    {
        var order = await Repository.Find(o => o.Id == orderId).FirstOrDefaultAsync() ??
                    throw new ModelNotFoundException($"Order not found with Id {orderId}");
        var mailRequest = new MailRequest()
        {
            Body = await RenderOrderToHtml(order),
            ToEmail = order.Customer?.UserInfo?.Email ?? "maidinhlinh967@gmail.com",
            Subject = "Your Order"
        };
        _mailingService.SendEmailAsync(mailRequest, true);
    }
}