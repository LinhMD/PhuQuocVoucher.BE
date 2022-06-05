using System.ComponentModel.DataAnnotations;

namespace PhuQuocVoucher.Data.Models;

public class ServiceProvider
{
    public int Id { get; set; }

    [Required]
    public string ProviderName { get; set; }

    public string? Address { get; set; }

    public int Status { get; set; }

    public string TaxCode { get; set; }

    public User ProviderUser { get; set; }


}