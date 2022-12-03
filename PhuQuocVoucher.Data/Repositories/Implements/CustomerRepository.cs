using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<Customer> IncludeAll()
    {
        return Models.Include(o => o.UserInfo)
            .Include(o => o.Orders)
            .Include(o => o.Reviews);
    }
}