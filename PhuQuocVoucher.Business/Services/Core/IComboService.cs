using CrudApiTemplate.Services;
using PhuQuocVoucher.Data.Dtos.ComboDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IComboService : IServiceCrud<Combo>
{
    public  Task<Combo> CreateAsync(CreateCombo createCombo);
}