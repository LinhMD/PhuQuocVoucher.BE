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

    public async Task<int> UpdateVoucherInventory()
    {
        return await Context.Database.ExecuteSqlRawAsync(
            $"UPDATE v Set v.Inventory = q.inventory from Vouchers  v LEFT JOIN (SELECT q.VoucherId, COUNT(q.Id) AS inventoryFROM Qrcodes q WHERE q.Status = GROUP BY q.VoucherId) AS q ON v.Id = q.VoucherId");
    }
}