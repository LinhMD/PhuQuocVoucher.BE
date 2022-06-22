using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ProviderTypeService : ServiceCrud<ProviderType>, IProviderTypeService
{
    public ProviderTypeService( IUnitOfWork work) : base(work.Get<ProviderType>(), work)
    {
    }
}