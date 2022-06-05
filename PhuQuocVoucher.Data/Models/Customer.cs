using System.ComponentModel.DataAnnotations;

namespace PhuQuocVoucher.Data.Models;

public class Customer
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string CustomerName { get; set; }

    public User? CustomerUser { get; set; }

}