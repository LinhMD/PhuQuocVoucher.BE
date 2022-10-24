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
    
    public ModelStatus Status { get; set; }
}