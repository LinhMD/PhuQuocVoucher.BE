using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<Order> IncludeAll()
    {
        return base.IncludeAll().Include(o => o.QrCodes);
    }

    public Task<Order> UpdateOrderAsync(Order orderToUpdate, int id)
    {
        return null;
    }
}