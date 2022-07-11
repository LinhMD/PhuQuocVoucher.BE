using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ServiceService : ServiceCrud<Service>, IServiceService
{
    private ILogger<ServiceService> _logger;
    public ServiceService( IUnitOfWork work, ILogger<ServiceService> logger) : base(work.Get<Service>(), work, logger)
    {
        _logger = logger;
    }
}