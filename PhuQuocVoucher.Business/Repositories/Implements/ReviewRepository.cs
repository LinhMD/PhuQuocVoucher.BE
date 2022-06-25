using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(DbContext context) : base(context)
    {
    }
}