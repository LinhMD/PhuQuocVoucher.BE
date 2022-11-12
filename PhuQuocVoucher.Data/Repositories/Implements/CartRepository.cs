using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

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