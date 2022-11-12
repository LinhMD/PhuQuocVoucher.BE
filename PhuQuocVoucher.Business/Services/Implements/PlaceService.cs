using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class PlaceService : ServiceCrud<Place>, IPlaceService
{
    private ILogger<PlaceService> _logger;
    public PlaceService(IUnitOfWork work, ILogger<PlaceService> logger) : base(work.Get<Place>(), work, logger)
    {
        _logger = logger;
    }
}