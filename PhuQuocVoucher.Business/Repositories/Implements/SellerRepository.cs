using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

public class SellerRepository : Repository<Seller>, ISellerRepository
{
    public SellerRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<Seller> IncludeAll()
    {
        return Models.Include(s => s.UserInfo);
    }
}