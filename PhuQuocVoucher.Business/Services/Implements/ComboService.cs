using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ComboService : ServiceCrud<Combo>, IComboService
{
    public ComboService(IUnitOfWork work) : base(work.Get<Combo>(), work)
    {
    }
}