using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ServiceService : ServiceCrud<Service>, IServiceService
{
    public ServiceService( IUnitOfWork work) : base(work.Get<Service>(), work)
    {
    }
}