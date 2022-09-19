using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.PriceLevelDto;

public class CreatePriceLevel : CreateDto, ICreateRequest<PriceLevel>
{
    public string Name { get; set; }
    
    public bool IsSellerPrice { get; set; }
}