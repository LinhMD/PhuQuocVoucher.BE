using CrudApiTemplate.View;
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.PriceLevelDto;

public class PriceLevelView : IView<PriceLevel>, IDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public bool IsSellerPrice { get; set; }
    
}