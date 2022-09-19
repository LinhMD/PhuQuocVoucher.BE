using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.PriceBookDto;

public class PriceBookView : IView<PriceBook>, IDto
{
    public int Id { get; set; }
    
    public string PriceLevelName { get; set; }
    
    public int PriceLevelId { get; set; }

    public int ProductId { get; set; }
    
    public double Price { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<PriceBook, PriceBookView>.NewConfig().Map(view => view.PriceLevelName, book => book.PriceLevel.Name);
    }
}