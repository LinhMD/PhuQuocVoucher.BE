using CrudApiTemplate.View;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.BlogDto;

public class BlogView : IView<Blog>, IDto
{

    public int Id { get; set; }

    public string Content { get; set; }

    public string BannerImage { get; set; }

    public string Title { get; set; }

    public string Summary { get; set; }

    public IEnumerable<Tag> Tags { get; set; }

    public IEnumerable<int> PlaceIds { get; set; }
}