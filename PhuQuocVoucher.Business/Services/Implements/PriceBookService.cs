using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class PriceBookService : ServiceCrud<PriceBook>, IPriceBookService
{
    private readonly ILogger<PriceBookService> _logger;

    public PriceBookService(IUnitOfWork work, ILogger<PriceBookService> logger) : base(work.Get<PriceBook>(), work, logger)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<PriceBookView>> CreateManyAsync(IEnumerable<CreatePriceBookSimple> priceBooks, int voucherId)
    {
        var prices = priceBooks.Select(p => new PriceBook
        {
            Price = p.Price, 
            VoucherId = voucherId, 
            PriceLevel = p.PriceLevel
        });
        await Repository.AddAllAsync(prices);
        return await Repository.Find<PriceBookView>(p => p.VoucherId == voucherId).ToListAsync();
    }

}