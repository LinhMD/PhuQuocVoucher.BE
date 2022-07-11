using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.BlogDto;

public class CreateBlog : CreateDto, ICreateRequest<Blog>
{

    [Required]
    public string Content { get; set; }

    [Required]
    public string BannerImage { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Summary { get; set; }

    public IEnumerable<int>? PlaceIds { get; set; }

    public IEnumerable<int>? TagIds { get; set; }

    public Blog CreateNew(IUnitOfWork work)
    {
        var result = this.Adapt<Blog>();

        result.Places = this.PlaceIds?.Select(i => new Place() {Id = i}) ?? Array.Empty<Place>();
        result.Tags = this.TagIds?.Select(i => new Tag() {Id = i}) ?? Array.Empty<Tag>();

        return result;
    }
}