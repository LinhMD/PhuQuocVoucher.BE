using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class RankRepository : Repository<SellerRank>, IRankRepository
{
    public RankRepository(DbContext context) : base(context)
    {
    }
}