using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<Customer> IncludeAll()
    {
        return Models.Include(o => o.UserInfo)
            .Include(o => o.Profiles)
            .Include(o => o.Orders)
            .Include(o => o.Reviews);
    }
}