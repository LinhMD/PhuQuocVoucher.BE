﻿using CrudApiTemplate.Repository;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Repositories.Core;

public interface IVoucherRepository : IRepository<Voucher>
{
    public Task<int> UpdateVoucherInventory();
}