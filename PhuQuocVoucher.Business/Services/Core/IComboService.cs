using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.ComboDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IComboService : IServiceCrud<Combo>
{
    public  Task<ComboView> CreateAsync(CreateCombo createCombo);

    public Task<(IList<ComboView>, int)> FindComboAsync(GetRequest<Combo> request, Role? role = Role.Customer);
}