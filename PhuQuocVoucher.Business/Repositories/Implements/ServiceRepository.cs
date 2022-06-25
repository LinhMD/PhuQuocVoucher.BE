using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

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