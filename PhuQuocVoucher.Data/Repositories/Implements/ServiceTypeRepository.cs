using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class ServiceTypeRepository : Repository<ServiceType>, IServiceTypeRepository
{
    public ServiceTypeRepository(DbContext context) : base(context)
    {
    }
    public override IQueryable<ServiceType> IncludeAll()
    {
        return Models.Include(s => s.ParentType);
    }
}