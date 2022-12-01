using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;


[Index(nameof(UserInfoId), IsUnique = true)]
public class ServiceProvider : BaseModel, IOrderAble
{
    public int Id { get; set; }

    [Required]
    public string? ProviderName { get; set; }

    public string? Address { get; set; }

    public string? TaxCode { get; set; }

    public User UserInfo { get; set; }
    public int UserInfoId { get; set; }

    public Seller? AssignedSeller { get; set; }

    public int? AssignedSellerId { get; set; }

    public IEnumerable<Service> Services { get; set; }
    
    public void ConfigOrderBy()
    {
        SetUpOrderBy<ServiceProvider>();
    }
}