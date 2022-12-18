using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IVoucherService : IServiceCrud<Voucher>
{
    public Task<VoucherView> CreateAsync(CreateVoucher createVoucher);

    public Task<VoucherView> UpdateTag(IList<int> tagIds, int voucherId);

    public Task UpdateInventory();

    public Task<VoucherView> UpdateVoucher(UpdateVoucher updateVoucher, int id);

    public Task UpdateVoucherInventoryList(IList<int> voucherIds, Dictionary<int, int>? comboQuantity = null);
}