using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(Name), IsUnique = true)]
public class Tag : BaseModel, IOrderAble
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public IList<Blog> Blogs { get; set; }
    
    public IList<Voucher> Compaign { get; set; }
    public void ConfigOrderBy()
    {
        SetUpOrderBy<Tag>();
    }
}