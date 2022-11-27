using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class Place  : BaseModel, IOrderAble
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string MapLocation { get; set; }

    public string Image { get; set; }

    public IEnumerable<Blog> Blogs { get; set; }

    public IEnumerable<Service> Services { get; set; }
    public void ConfigOrderBy()
    {
        Expression<Func<Place, ModelStatus>> orderByStatus = place => place.Status;
        OrderByProvider<Place>.OrderByDic.Add(nameof(Status),orderByStatus);
    }
}