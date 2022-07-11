using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class CartService : ServiceCrud<Cart>, ICartService
{
    private ILogger<CartService> _logger;
    public CartService(IUnitOfWork work, ILogger<CartService> logger) : base(work.Get<Cart>(), work, logger)
    {
        _logger = logger;
    }
}