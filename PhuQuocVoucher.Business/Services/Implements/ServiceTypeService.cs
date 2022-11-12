using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;
using ServiceProvider = Microsoft.Extensions.DependencyInjection.ServiceProvider;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ServiceTypeService : ServiceCrud<ServiceType>, IServiceTypeService
{
    private ILogger<ServiceTypeService> _logger;
    public ServiceTypeService( IUnitOfWork work, ILogger<ServiceTypeService> logger) : base(work.Get<ServiceType>(), work, logger)
    {
        _logger = logger;
    }
}