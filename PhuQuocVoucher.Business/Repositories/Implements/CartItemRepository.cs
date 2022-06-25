using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

public class CartItemRepository : Repository<CartItem>, ICartItemRepository
{
    public CartItemRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<CartItem> IncludeAll()
    {
        return Models.Include(o => o.Product);
    }
}