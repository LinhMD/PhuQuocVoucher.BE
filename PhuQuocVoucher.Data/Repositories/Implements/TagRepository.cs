using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(DbContext context) : base(context)
    {
    }
    public override IQueryable<Tag> IncludeAll()
    {
        return Models;
    }
}