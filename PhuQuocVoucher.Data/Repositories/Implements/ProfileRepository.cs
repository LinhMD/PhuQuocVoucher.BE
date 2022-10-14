using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

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