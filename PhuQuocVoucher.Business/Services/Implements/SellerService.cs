using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.MailDto;
using PhuQuocVoucher.Business.Dtos.MomoDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Business.Dtos.UserDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class SellerService : ServiceCrud<Seller>, ISellerService
{
    private ILogger<SellerService> _logger;

    public SellerService(IUnitOfWork work, ILogger<SellerService> logger) : base(work.Get<Seller>(), work, logger)
    {
        _logger = logger;
    }

    public async Task<(IList<SellerView>, int)> FindSellerAsync(GetRequest<Seller> request,
        DateTime? completeDateLowBound)
    {
        var filter = Repository.GetAll().Where(request.FindRequest.ToPredicate());

        var total = await filter.CountAsync();

        var result = filter
            .OrderBy(request.OrderRequest)
            .Paging(request.GetPaging());

        var sellerViews = result.Select(s => new SellerView()
        {
            Id = s.Id,
            Orders = s.HandleOrders
                .Where(o => (o.CompleteDate >= completeDateLowBound))
                .Select(o => o.Adapt<OrderView>()),
            Profit = s.HandleOrders
                .Where(o => (o.CompleteDate >= completeDateLowBound))
                .Select(o => o.TotalPrice).Sum(),
            BusyLevel = s.BusyLevel,
            CommissionRate = s.CommissionRate,
            SellerName = s.SellerName,
            UserInfo = s.UserInfo.Adapt<UserView>(),
            UserInfoId = s.UserInfoId
        });

        return (await sellerViews.ToListAsync(), total);
    }

    public IList<SellerView> CreateSellers(IList<CreateSeller> createSellers)
    {
        
        return null;
    }

    public async Task<SellerKpiView> GetSellerKpis(int sellerId, int month, int year)
    {
        var orderItems = await UnitOfWork.Get<OrderItem>().Find(item => item.CreateAt != null && item.SellerId == sellerId && 
                                                                        item.CreateAt.Value.Month == month &&
                                                                        item.CreateAt.Value.Year == year).ToListAsync();
        
        var orders = await UnitOfWork.Get<Order>()
            .Find(order => order.CreateAt != null && 
                           order.SellerId == sellerId && 
                           order.CreateAt.Value.Month == month &&
                           order.CreateAt.Value.Year == year && 
                           (order.OrderStatus == OrderStatus.Completed || order.OrderStatus == OrderStatus.Used))
            .ToListAsync();

        var customers = await UnitOfWork.Get<Customer>().Find(c => c.CreateAt != null && c.AssignSellerId == sellerId && 
                                                                   c.CreateAt.Value.Month == month &&
                                                                   c.CreateAt.Value.Year == year).ToListAsync();

        var closeOrder = orders.Count;
        var amount = orderItems.Select(item => item.SellerRate).Sum();
        var newCustomer = customers.Count;
        
        var sellerKpi = new SellerKpiView()
        {
            CloseOrder = closeOrder,
            Revenue = amount,
            SellerId = sellerId,
            NoOfNewCustomer = newCustomer
        };
        return sellerKpi;
    }
}