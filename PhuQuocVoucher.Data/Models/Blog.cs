using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class Blog : BaseModel, IOrderAble
{
    public int Id { get; set; }

    public string Content { get; set; }

    public string BannerImage { get; set; }

    public string Title { get; set; }

    public string Summary { get; set; }

    public IEnumerable<Place> Places { get; set; }

    public IEnumerable<Tag>? Tags { get; set; }
    public void ConfigOrderBy()
    {
        Expression<Func<Blog, ModelStatus>> orderByStatus = blog => blog.Status;
        OrderByProvider<Blog>.OrderByDic.Add(nameof(Status),orderByStatus);
    }
}