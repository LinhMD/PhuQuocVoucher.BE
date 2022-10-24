using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.PriceLevelDto;

public class FindPriceLevel : IFindRequest<PriceLevel>
{
    public int? Id { get; set; }
    
    [Contain]
    public string? Name { get; set; }
    
    public bool? IsSellerPrice { get; set; }

    public ModelStatus? Status { get; set; }

    //TODO : 
}