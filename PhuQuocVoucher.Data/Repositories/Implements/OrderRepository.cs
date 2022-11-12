using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

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


    public async Task<Order> UpdateOrderAsync(Order orderToUpdate, int id)
    {
        var order = await Models.FindAsync(id);

        if(order != null)
            order.OrderStatus = OrderStatus.Canceled;
        
        await this.CommitAsync(); //luu DB

        return order;
    }
}