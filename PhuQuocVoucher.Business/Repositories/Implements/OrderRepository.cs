using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

public class OrderRepository : Repository<Order> , IOrderRepository
{
    public OrderRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<Order> IncludeAll()
    {
        return Models.Include(o => o.Customer)
            .Include(o => o.Seller)
            .Include(o => o.PaymentDetail)
            .Include(o => o.OrderItems);
    }
}