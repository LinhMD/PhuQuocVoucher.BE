using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(DbContext context) : base(context)
    {
    }

}