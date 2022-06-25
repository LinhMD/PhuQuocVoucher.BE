using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

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