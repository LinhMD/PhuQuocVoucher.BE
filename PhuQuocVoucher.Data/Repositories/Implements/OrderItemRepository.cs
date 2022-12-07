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
}