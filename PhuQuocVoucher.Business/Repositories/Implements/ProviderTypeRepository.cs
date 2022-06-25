using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

public class ProviderTypeRepository : Repository<ProviderType>, IProviderTypeRepository
{
    public ProviderTypeRepository(DbContext context) : base(context)
    {
    }
}