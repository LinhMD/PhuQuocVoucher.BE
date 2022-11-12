using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ProviderService : ServiceCrud<ServiceProvider>, IProviderService
{
    private ILogger<ProviderService> _logger;
    public ProviderService( IUnitOfWork work, ILogger<ProviderService> logger) : base(work.Get<ServiceProvider>(), work, logger)
    {
        _logger = logger;
    }
}