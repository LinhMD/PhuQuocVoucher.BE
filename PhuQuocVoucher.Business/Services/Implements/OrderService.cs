using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class OrderService : ServiceCrud<Order>, IOrderService
{
    private ILogger<OrderService> _logger;
    public OrderService(IUnitOfWork work, ILogger<OrderService> logger) : base(work.Get<Order>(), work, logger)
    {
        _logger = logger;
    }
}