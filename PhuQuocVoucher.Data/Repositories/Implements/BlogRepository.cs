using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class BlogRepository : Repository<Blog>, IBlogRepository
{
    public BlogRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<Blog> IncludeAll()
    {
        return Models.Include(o => o.Places)
            .Include(o => o.Tags);
    }
}