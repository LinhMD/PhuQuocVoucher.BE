using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

public class ProviderRepository : Repository<ServiceProvider>, IProviderRepository
{
    public ProviderRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<ServiceProvider> IncludeAll()
    {
        return Models
            .Include(s => s.UserInfo)
            .Include(s => s.AssignedSeller)
            .Include(s => s.Type);
    }
}