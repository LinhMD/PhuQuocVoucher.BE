using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class PriceLevelRepository : Repository<PriceLevelT>, IPriceLevelRepository
{
    public PriceLevelRepository(DbContext context) : base(context)
    {
    }
}