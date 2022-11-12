using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class OrderItemService : ServiceCrud<OrderItem>, IOrderItemService
{
    private ILogger<OrderItemService> _logger;
    public OrderItemService(IUnitOfWork work, ILogger<OrderItemService> logger) : base(work.Get<OrderItem>(), work, logger)
    {
        _logger = logger;
    }
}