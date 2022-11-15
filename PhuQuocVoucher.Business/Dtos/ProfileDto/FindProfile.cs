using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProfileDto;

public class FindProfile : IFindRequest<Profile>
{
    public int? Sex { get; set; }

    [Contain]
    public string? PhoneNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [Contain]
    public string? Name { get; set; }

    [Contain]
    public string? CivilIdentify { get; set; }

    public int? CustomerId { get; set; }
    [BiggerThan(nameof(BaseModel.UpdateAt))]
    public DateTime? UpdateAt_startTime { get; set; }
    
    [LessThan(nameof(BaseModel.UpdateAt))]
    public DateTime? UpdateAt_endTime { get; set; }

    [BiggerThan(nameof(BaseModel.CreateAt))]
    public DateTime? CreateAt_startTime { get; set; }
    
    [LessThan(nameof(BaseModel.CreateAt))]
    public DateTime? CrateAt_endTime { get; set; }

}