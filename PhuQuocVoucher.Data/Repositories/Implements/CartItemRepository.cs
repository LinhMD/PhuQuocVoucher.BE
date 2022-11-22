using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class CartItemRepository : Repository<CartItem>, ICartItemRepository
{
    public CartItemRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<CartItem> IncludeAll()
    {
        return Models.AsQueryable()
            .Include(item => item.Voucher)
            .Include(item => item.Price)
            .Include(item => item.Profile);
    }
}