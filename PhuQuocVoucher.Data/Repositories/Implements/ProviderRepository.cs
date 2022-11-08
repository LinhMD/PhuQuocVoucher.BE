using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class ProviderRepository : Repository<ServiceProvider>, IProviderRepository
{
    public ProviderRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<ServiceProvider> IncludeAll()
    {
        return Models
            .Include(s => s.UserInfo)
            .Include(s => s.AssignedSeller);

    }
}