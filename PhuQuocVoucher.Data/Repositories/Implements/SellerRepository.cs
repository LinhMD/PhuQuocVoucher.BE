using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class SellerRepository : Repository<Seller>, ISellerRepository
{
    public SellerRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<Seller> IncludeAll()
    {
        return Models.Include(s => s.UserInfo).Include(s => s.HandleOrders);
    }


}