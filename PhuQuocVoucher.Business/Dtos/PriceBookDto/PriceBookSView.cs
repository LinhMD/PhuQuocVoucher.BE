using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.PriceBookDto;

public class PriceBookSView : IView<PriceBook>, IDto
{
    public int Id { get; set; }
    public string PriceLevelName { get; set; }

    public double Price { get; set; }
    
    public bool IsDefault { get; set; }

    public void InitMapper()
    {
    }
}