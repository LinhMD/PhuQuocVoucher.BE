using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IPriceBookService : IServiceCrud<PriceBook>
{
    public Task<IEnumerable<PriceBookView>> CreateManyAsync(IEnumerable<CreatePriceBookSimple> priceBooks, int productId);
}