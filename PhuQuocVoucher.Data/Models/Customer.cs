using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(UserInfoId), IsUnique = true)]
public class Customer : BaseModel, IOrderAble
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string CustomerName { get; set; }
    
    public Cart? Cart { get; set; }
    
    public int? CartId { get; set; }

    public User? UserInfo { get; set; }
    public int? UserInfoId { get; set; }
    
    public Seller? AssignSeller { get; set; }
    
    public int? AssignSellerId { get; set; }

    public IEnumerable<Profile> Profiles { get; set; }

    public IEnumerable<Order> Orders { get; set; }

    public IEnumerable<Review> Reviews { get; set; }
    
    public void ConfigOrderBy()
    {
        
        SetUpOrderBy<Customer>();
    }

}