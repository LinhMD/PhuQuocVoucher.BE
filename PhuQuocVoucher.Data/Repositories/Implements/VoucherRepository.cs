using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class VoucherRepository : Repository<Voucher>, IVoucherRepository
{
    public VoucherRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<Voucher> IncludeAll()
    {
        return Models
            .Include(v => v.Service);
    }
}