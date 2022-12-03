using CrudApiTemplate.Repository;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Repositories.Core;

public interface IVoucherRepository : IRepository<VoucherCompaign>
{
    public Task<int> UpdateVoucherInventory();
}