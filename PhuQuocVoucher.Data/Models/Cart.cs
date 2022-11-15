using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(CustomerId), IsUnique = true)]
public class Cart : BaseModel, IOrderAble
{
    public int Id { get; set; }

    public Customer Customer { get; set; }

    public int CustomerId { get; set; }

    public IList<CartItem> CartItems { get; set; }
    
    public void ConfigOrderBy()
    {
        Expression<Func<Cart, ModelStatus>> orderByStatus = cart => cart.Status;
        OrderByProvider<Cart>.OrderByDic.Add(nameof(Status),orderByStatus);
    }
}