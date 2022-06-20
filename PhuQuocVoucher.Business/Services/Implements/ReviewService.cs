using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ReviewService : ServiceCrud<Review>, IReviewService
{
    public ReviewService( IUnitOfWork work) : base(work.Get<Review>(), work)
    {
    }
}