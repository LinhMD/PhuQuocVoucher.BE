using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class PriceLevelService : ServiceCrud<PriceLevel>, IPriceLevelService
{
    private readonly ILogger<PriceLevelService> _logger;

    public PriceLevelService( IUnitOfWork work, ILogger<PriceLevelService> logger) 
        : base(work.Get<PriceLevel>(), work, logger)
    {
        _logger = logger;
    }
    
    
}