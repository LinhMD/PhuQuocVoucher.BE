using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ProductService : ServiceCrud<Product>, IProductService
{
    private ILogger<ProductService> _logger;
    public ProductService(IUnitOfWork work, ILogger<ProductService> logger) : base(work.Get<Product>(), work, logger)
    {
        _logger = logger;
    }
}