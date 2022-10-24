using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.PriceBookDto;

public class FindPriceBook : IFindRequest<PriceBook>
{
    [Equal]
    public int? Id { get; set; }
    
    [Equal]
    public int? PriceLevelId { get; set; }

    [Equal]
    public int? ProductId { get; set; }
    
    [BiggerThan(nameof(PriceBook.Price))]
    public double? PriceLow { get; set; }
    
    [LessThan(nameof(PriceBook.Price))]
    public double? PriceHigh { get; set; }

    public ModelStatus Status { get; set; }

}