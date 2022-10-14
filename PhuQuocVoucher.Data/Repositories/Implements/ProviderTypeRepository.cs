using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class ProviderTypeRepository : Repository<ProviderType>, IProviderTypeRepository
{
    public ProviderTypeRepository(DbContext context) : base(context)
    {
    }
}