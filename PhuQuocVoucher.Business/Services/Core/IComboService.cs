using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.ComboDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IComboService : IServiceCrud<Voucher>
{
    public Task<ComboView> UpdateTag(IList<int> tagIds, int voucherId);


    public Task<ComboView> CreateCombo(CreateCombo createCombo);
}