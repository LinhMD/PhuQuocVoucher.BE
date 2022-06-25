using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

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