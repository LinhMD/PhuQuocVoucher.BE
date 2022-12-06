using CrudApiTemplate.Repository;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Repositories.Core;

public interface IVoucherRepository : IRepository<Voucher>
{
    public Task<int> UpdateVoucherInventoryList(IList<int> voucherIds);
    public Task<int> UpdateVoucherInventory();
}