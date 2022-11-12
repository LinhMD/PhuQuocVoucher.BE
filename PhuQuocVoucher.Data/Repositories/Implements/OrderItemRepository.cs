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
        return Models.Include(o => o.Order)
            .Include(o => o.QrCode)
            .Include(o => o.Profile)
            .Include(o => o.Review);
    }
}