using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(UserInfoId), IsUnique = true)]
public class Seller : BaseModel
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
}
