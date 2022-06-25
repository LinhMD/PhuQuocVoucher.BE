using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

public class OrderItemRepository : Repository<OrderItem> , IOrderItemRepository
{
    public OrderItemRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<OrderItem> IncludeAll()
    {
        return Models.Include(o => o.Order)
            .Include(o => o.OrderProduct)
            .Include(o => o.Profile)
            .Include(o => o.Review);
    }
}