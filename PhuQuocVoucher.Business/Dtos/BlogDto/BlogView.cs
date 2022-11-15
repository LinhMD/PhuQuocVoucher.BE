using CrudApiTemplate.View;
using PhuQuocVoucher.Business.Dtos.PlaceDto;
using PhuQuocVoucher.Business.Dtos.TagDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.BlogDto;

public class BlogView : IView<Blog>, IDto
{

    public int Id { get; set; }

    public string Content { get; set; }

    public string BannerImage { get; set; }

    public string Title { get; set; }

    public string Summary { get; set; }

    public IEnumerable<PlaceSView> Places { get; set; }

    public IEnumerable<TagView>? Tags { get; set; }
}