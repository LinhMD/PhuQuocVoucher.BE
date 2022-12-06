using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.TagDto;

public class FindTag : IFindRequest<Tag>
{
    public int? Id { get; set; }

    [Contain] public string? Name { get; set; }

    [BiggerThan(nameof(BaseModel.UpdateAt))]
    public DateTime? UpdateAt_startTime { get; set; }

    [LessThan(nameof(BaseModel.UpdateAt))] public DateTime? UpdateAt_endTime { get; set; }

    [BiggerThan(nameof(BaseModel.CreateAt))]
    public DateTime? CreateAt_startTime { get; set; }

    [LessThan(nameof(BaseModel.CreateAt))] public DateTime? CrateAt_endTime { get; set; }


    public ModelStatus? Status { get; set; }
}