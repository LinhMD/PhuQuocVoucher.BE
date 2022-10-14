using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class ServiceRepository : Repository<Service>, IServiceRepository
{
    public ServiceRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<Service> IncludeAll()
    {
        return Models
            .Include(s => s.Provider)
            .Include(s => s.Type)
            .Include(s => s.ServiceLocation);
    }
}