using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ProviderTypeService : ServiceCrud<ProviderType>, IProviderTypeService
{
    private ILogger<ProviderTypeService> _logger;
    public ProviderTypeService(IUnitOfWork work, ILogger<ProviderTypeService> logger) : base(work.Get<ProviderType>(), work, logger)
    {
        _logger = logger;
    }
}