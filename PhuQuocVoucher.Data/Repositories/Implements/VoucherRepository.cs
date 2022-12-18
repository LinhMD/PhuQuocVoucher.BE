﻿using CrudApiTemplate.Repository;
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

    public async Task<int> UpdateVoucherInventoryList(IList<int> voucherIds)
    {
        var aggregate = string.Join(",", voucherIds);
        await Context.Database.ExecuteSqlRawAsync(
            $"update v set v.Inventory = (case when q.inventory is null then 0 else q.inventory end) from vouchers v left JOIN( SELECT q.VoucherId, COUNT(q.Id) AS inventory FROM QrCodes q WHERE q.QrCodeStatus = 0 GROUP BY q.VoucherId) AS q ON v.Id = q.VoucherId where v.IsCombo = 0 and v.Id in ({aggregate})");
        
        return 0;
    }
    public async Task<int> UpdateVoucherInventory()
    {
        await Context.Database.ExecuteSqlRawAsync(
            $" update v set v.Inventory = (case when q.inventory is null then 0 else q.inventory end) from vouchers v left JOIN( SELECT q.VoucherId, COUNT(q.Id) AS inventory FROM QrCodes q WHERE q.QrCodeStatus = 0 GROUP BY q.VoucherId) AS q ON v.Id = q.VoucherId where v.IsCombo = 0 ");
       
        return 0;
    }
} 