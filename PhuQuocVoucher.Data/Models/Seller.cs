using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(UserInfoId), IsUnique = true)]
public class Seller : BaseModel, IOrderAble

{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string SellerName { get; set; }

    public User UserInfo { get; set; }

    public int UserInfoId { get; set; }

    public float CommissionRate { get; set; }

    public BusyLevel BusyLevel { get; set; } = BusyLevel.Free;

    public IEnumerable<Order> HandleOrders { get; set; }

    public IEnumerable<Customer> Customers { get; set; }
    public void ConfigOrderBy()
    {
        
        SetUpOrderBy<Seller>();
        Expression<Func<Seller, BusyLevel>> orderByBusyLevel = seller => seller.BusyLevel;
        OrderByProvider<Seller>.OrderByDic.Add(nameof(BusyLevel), orderByBusyLevel);
    }
}
