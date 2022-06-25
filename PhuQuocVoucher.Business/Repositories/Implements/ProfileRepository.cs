using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

public class ProfileRepository : Repository<Profile>, IProfileRepository
{
    public ProfileRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<Profile> IncludeAll()
    {
        return Models.Include(p => p.Customer);
    }
}