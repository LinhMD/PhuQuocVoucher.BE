using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class OrderItemService : ServiceCrud<OrderItem>, IOrderItemService
{
    public OrderItemService(IUnitOfWork work) : base(work.Get<OrderItem>(), work)
    {
    }
}