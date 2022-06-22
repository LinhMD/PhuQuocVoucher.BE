using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ProductService : ServiceCrud<Product>, IProductService
{
    public ProductService(IUnitOfWork work) : base(work.Get<Product>(), work)
    {
    }
}