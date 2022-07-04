using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.BlogDto;

public class FindBlog : IFindRequest<Blog>, IDto
{
    [Equal]
    public int? Id { get; set; }

    [Contain]
    public string? Content { get; set; }

    [Contain]
    public string? Title { get; set; }

    [Contain]
    public string? Summary { get; set; }

    [Any("Places", "Id", typeof(EqualAttribute))]
    public int? AnyPlaceId { get; set; }


    [Any("Tags", "Id", typeof(EqualAttribute))]
    public int? AnyTagId { get; set; }
}