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
    public int? VoucherId { get; set; }
    
    [BiggerThan(nameof(PriceBook.Price))]
    public double? PriceLow { get; set; }
    
    [LessThan(nameof(PriceBook.Price))]
    public double? PriceHigh { get; set; }
    
    [BiggerThan(nameof(BaseModel.UpdateAt))]
    public DateTime? UpdateAt_startTime { get; set; }
    
    [LessThan(nameof(BaseModel.UpdateAt))]
    public DateTime? UpdateAt_endTime { get; set; }

    [BiggerThan(nameof(BaseModel.CreateAt))]
    public DateTime? CreateAt_startTime { get; set; }
    
    [LessThan(nameof(BaseModel.CreateAt))]
    public DateTime? CrateAt_endTime { get; set; }

}