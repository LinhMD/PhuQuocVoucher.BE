using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class OrderService : ServiceCrud<Order>, IOrderService
{
    public OrderService(IUnitOfWork work) : base(work.Get<Order>(), work)
    {
    }
}