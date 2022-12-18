using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class OrderItemRepository : Repository<OrderItem> , IOrderItemRepository
{
    public OrderItemRepository(DbContext context) : base(context)
    {
    }
    public override IQueryable<OrderItem> IncludeAll()
    {
        return base.IncludeAll().Include(o => o.QrCodes);
    }
}