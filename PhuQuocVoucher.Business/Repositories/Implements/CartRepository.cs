using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

public class CartRepository : Repository<Cart>, ICartRepository
{
    public CartRepository(DbContext context) : base(context)
    {
    }


    public override IQueryable<Cart> IncludeAll()
    {
        return Models.Include(o => o.Customer)
            .Include(o => o.CartItems);
    }
}