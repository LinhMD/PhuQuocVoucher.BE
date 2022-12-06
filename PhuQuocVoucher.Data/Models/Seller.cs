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

    public IEnumerable<Order> HandleOrders { get; set; }

    public IEnumerable<Customer> Customers { get; set; }

    public IList<SellerActivity> Activities { get; set; }
    
    public SellerRank Rank { get; set; }

    public int RankId { get; set; }
    
    public void ConfigOrderBy()
    {
        SetUpOrderBy<Seller>();
    }
}
