using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class SellerService : ServiceCrud<Seller>, ISellerService
{
    private ILogger<SellerService> _logger;
    public SellerService( IUnitOfWork work, ILogger<SellerService> logger) : base(work.Get<Seller>(), work, logger)
    {
        _logger = logger;
    }
}