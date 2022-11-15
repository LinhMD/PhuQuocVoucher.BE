using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.BlogDto;

public class FindBlog : IFindRequest<Blog>, IDto
{
    [Equal]
    public int? Id { get; set; }

    [Contain]
    public string? Content { get; set; }

    [Contain(nameof(Blog.Title))]
    public string? Title { get; set; }

    [Contain]
    public string? Summary { get; set; }

    [Any(nameof(Blog.Places), nameof(Place.Id), typeof(EqualAttribute))]
    public int? AnyPlaceId { get; set; }

    [Any(nameof(Blog.Tags), nameof(Tag.Id), typeof(EqualAttribute))]
    public int? AnyTagId { get; set; }
    
    [BiggerThan(nameof(BaseModel.UpdateAt))]
    public DateTime? UpdateAt_startTime { get; set; }
    
    [LessThan(nameof(BaseModel.UpdateAt))]
    public DateTime? UpdateAt_endTime { get; set; }

    [BiggerThan(nameof(BaseModel.CreateAt))]
    public DateTime? CreateAt_startTime { get; set; }
    
    [LessThan(nameof(BaseModel.CreateAt))]
    public DateTime? CrateAt_endTime { get; set; }

}