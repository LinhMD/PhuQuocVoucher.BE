using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class ComboRepository : Repository<Combo>, IComboRepository
{
    public ComboRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<Combo> IncludeAll()
    {
        return Models.Include(o => o.Product)
            .Include(o => o.Vouchers);
    }
}