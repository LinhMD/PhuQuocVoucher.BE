using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ReviewService : ServiceCrud<Review>, IReviewService
{
    private ILogger<ReviewService> _logger;
    public ReviewService( IUnitOfWork work, ILogger<ReviewService> logger) : base(work.Get<Review>(), work, logger)
    {
        _logger = logger;
    }
}