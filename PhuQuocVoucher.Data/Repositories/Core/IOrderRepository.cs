using CrudApiTemplate.Repository;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Repositories.Core;

public interface IOrderRepository : IRepository<Order>
{
    public Task<Order> UpdateOrderAsync(Order orderToUpdate, int id);
}