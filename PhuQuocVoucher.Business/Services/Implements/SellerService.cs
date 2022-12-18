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
using PhuQuocVoucher.Business.Dtos.RankDto;
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

        /*var result = filter
            .OrderBy(request.OrderRequest)
            .Paging(request.GetPaging());*/
        var result = default(IQueryable<Seller>);
        try
        {
            result = filter
                .Paging(request.GetPaging());;
        }
        catch (Exception e)
        {
            result =  filter
                .Paging(request.GetPaging());
        }
        var sellerViews = result.Select(s => new SellerView()
        {
            Id = s.Id,
            Orders = s.HandleOrders
                .Where(o => (o.CompleteDate >= completeDateLowBound))
                .Select(o => o.Adapt<OrderView>()),
            CommissionRate = s.Rank!.CommissionRatePercent,
            SellerName = s.SellerName,
            UserInfo = s.UserInfo.Adapt<UserView>(),
            UserInfoId = s.UserInfoId,
            Status = s.Status,
            Rank = new RankView()
            {
                Id = s.Rank.Id,
                Logo = s.Rank.Logo,
                Rank = s.Rank.Rank,
                EpxRequired = s.Rank.EpxRequired,
                CommissionRatePercent = s.Rank.CommissionRatePercent
            },
            Exp = s.Exp,
            CreateAt = s.CreateAt
        });

        return (await sellerViews.ToListAsync(), total);
    }

    public IList<SellerView> CreateSellers(IList<CreateSeller> createSellers)
    {
        
        return null;
    }

    public async Task<SellerKpiView> GetSellerKpis(int sellerId, int year)
    {
        var orderItems = (await UnitOfWork.Get<OrderItem>()
            .Find(item => item.CreateAt != null && 
                          item.SellerId == sellerId && 
                          item.CreateAt.Value.Year == year && 
                          (item.Order.OrderStatus == OrderStatus.Completed || item.Order.OrderStatus == OrderStatus.Used))
            .Select(item => new
            {
                item.CreateAt.Value.Date.Month,
                item.SellerCommission
            })
            .ToListAsync()).GroupBy(order => order.Month)
            .ToDictionary( g => g.Key, g => g.ToList().Sum(arg => arg.SellerCommission));
        
        
        var orders = (await UnitOfWork.Get<Order>()
            .Find(order => order.CreateAt != null && 
                           order.SellerId == sellerId && 
                           order.CreateAt.Value.Year == year && 
                           (order.OrderStatus == OrderStatus.Completed || order.OrderStatus == OrderStatus.Used))
            .Select(order => new
            {
                order.CreateAt.Value.Date.Month,
                order.Id
            })
            .ToListAsync())
            .GroupBy(order => order.Month)
            .ToDictionary( g => g.Key, g => g.Count());

        var customers = (await UnitOfWork.Get<Customer>()
            .Find(c => c.CreateAt != null && 
                       c.AssignSellerId == sellerId && 
                       c.CreateAt.Value.Year == year)
            .Select(customer => new
            {
                customer.CreateAt.Value.Date.Month,
                customer.Id
            })
            .ToListAsync())
            .GroupBy(customer => customer.Month)
            .ToDictionary( g => g.Key, g => g.Count());
        

        
        var sellerKpi = new SellerKpiView()
        {
            CloseOrderPerMonth = orders,
            SellerId = sellerId,
            NoOfNewCustomerPerMonth = customers,
            RevenuesPerMonths = orderItems
        };
        return sellerKpi;
    }
}