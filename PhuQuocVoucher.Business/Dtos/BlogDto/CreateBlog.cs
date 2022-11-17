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

}