using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;
using ServiceProvider = Microsoft.Extensions.DependencyInjection.ServiceProvider;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ServiceTypeService : ServiceCrud<ServiceType>, IServiceTypeService
{
    public ServiceTypeService( IUnitOfWork work) : base(work.Get<ServiceType>(), work)
    {
    }
}