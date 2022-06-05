using System.ComponentModel.DataAnnotations;

namespace PhuQuocVoucher.Data.Models;

public class Seller
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public User SellerUser { get; set; }

    public float CommissionRate { get; set; }

}