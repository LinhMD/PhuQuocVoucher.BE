using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(DbContext context) : base(context)
    {
    }
}