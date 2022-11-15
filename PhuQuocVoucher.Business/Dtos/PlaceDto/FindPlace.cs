using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.PlaceDto;

public class FindPlace : IFindRequest<Place>
{
    [Equal]
    public int? Id { get; set; }

    [Contain(nameof(Place.Name))]
    public string? Name { get; set; }

    [Contain]
    public string? MapLocation { get; set; }
    
    [BiggerThan(nameof(BaseModel.UpdateAt))]
    public DateTime? UpdateAt_startTime { get; set; }
    
    [LessThan(nameof(BaseModel.UpdateAt))]
    public DateTime? UpdateAt_endTime { get; set; }

    [BiggerThan(nameof(BaseModel.CreateAt))]
    public DateTime? CreateAt_startTime { get; set; }
    
    [LessThan(nameof(BaseModel.CreateAt))]
    public DateTime? CrateAt_endTime { get; set; }

}