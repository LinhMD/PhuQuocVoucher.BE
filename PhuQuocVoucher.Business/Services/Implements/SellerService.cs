using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Business.Dtos.UserDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class SellerService : ServiceCrud<Seller>, ISellerService
{
    private ILogger<SellerService> _logger;
    public SellerService( IUnitOfWork work, ILogger<SellerService> logger) : base(work.Get<Seller>(), work, logger)
    {
        _logger = logger;
    }

    public async Task<(IList<SellerView>, int)> FindSellerAsync(GetRequest<Seller> request, DateTime? completeDateLowBound)
    {
        var filter = Repository.GetAll().Where(request.FindRequest.ToPredicate());

        var total = filter.Count();

        var result = filter
            .OrderBy(request.OrderRequest)
            .Paging(request.GetPaging());

        var sellerViews = result.Select(s => new SellerView()
        {
            Id = s.Id,
            Orders = s.HandleOrders
                .Where(o => (o.CompleteDate >= completeDateLowBound))
                .Select(o => o.Adapt<OrderView>())
            ,
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
}