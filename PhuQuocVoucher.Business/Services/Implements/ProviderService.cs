using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ProviderService : ServiceCrud<ServiceProvider>, IProviderService
{
    public ProviderService( IUnitOfWork work) : base(work.Get<ServiceProvider>(), work)
    {
    }
}