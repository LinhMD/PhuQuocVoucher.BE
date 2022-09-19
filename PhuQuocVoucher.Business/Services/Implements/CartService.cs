using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Google.Apis.Json;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.CartDto;
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

    public async Task<CartView> GetCartByCustomerAsync(int customerId)
    {
        return await Repository.Find<CartView>(c => c.CustomerId == customerId).FirstOrDefaultAsync() ?? 
               (await Repository.AddAsync(new Cart
        {
            Status = ModelStatus.Active,
            CreateAt = DateTime.Now,
            CustomerId = customerId
        })).Adapt<CartView>();
    }
}