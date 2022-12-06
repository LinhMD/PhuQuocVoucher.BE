using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.BlogDto;

public class UpdateBlog : UpdateDto, IUpdateRequest<Blog>
{
    public string? Content { get; set; }

    public string? BannerImage { get; set; }

    public string? Title { get; set; }

    public string? Summary { get; set; }

    public List<int>? PlaceIds { get; set; }

    public List<int>? TagIds { get; set; }


    public ModelStatus? Status { get; set; }
}