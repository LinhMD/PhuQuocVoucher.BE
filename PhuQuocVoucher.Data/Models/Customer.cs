using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(UserInfoId), IsUnique = true)]
public class Customer
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string CustomerName { get; set; }

    public User? UserInfo { get; set; }

    public int? UserInfoId { get; set; }

    public IEnumerable<Profile> Profiles { get; set; }

    public IEnumerable<Order> Orders { get; set; }

    public IEnumerable<Review> Reviews { get; set; }

}