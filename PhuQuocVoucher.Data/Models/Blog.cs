using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class Blog : BaseModel, IOrderAble
{
    public int Id { get; set; }

    public string Content { get; set; }

    public string BannerImage { get; set; }

    public string Title { get; set; }

    public string Summary { get; set; }

    public IList<BlogPlace> Places { get; set; }

    public IList<BlogTag> Tags { get; set; }
    public void ConfigOrderBy()
    {
        SetUpOrderBy<Blog>();
    }
}