using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class CartItemService : ServiceCrud<CartItem>, ICartItemService
{
    private ILogger<CartItemService> _logger;
    public CartItemService(IUnitOfWork work, ILogger<CartItemService> logger) : base(work.Get<CartItem>(), work, logger)
    {
        _logger = logger;
    }
}