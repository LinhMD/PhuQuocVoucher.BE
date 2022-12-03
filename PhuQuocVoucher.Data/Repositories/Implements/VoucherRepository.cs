using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;


namespace PhuQuocVoucher.Data.Repositories.Implements;

public class VoucherRepository : Repository<VoucherCompaign>, IVoucherRepository
{
    public VoucherRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<VoucherCompaign> IncludeAll()
    {
        return Models
            .Include(v => v.Service);
    }

    public async Task<int> UpdateVoucherInventory()
    {
        return await Context.Database.ExecuteSqlRawAsync(
            $"UPDATE v SET v.Inventory = q.inventory FROM Vouchers v LEFT JOIN ( SELECT q.VoucherId, COUNT(q.Id) AS inventory FROM QrCodes q WHERE q.Status = 0 GROUP BY q.VoucherId ) AS q ON v.Id = q.VoucherId ");
    }
} 