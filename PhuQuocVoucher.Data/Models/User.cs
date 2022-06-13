using System.ComponentModel.DataAnnotations;

namespace PhuQuocVoucher.Data.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string? FireBaseUid { get; set; }

    public int Status { get; set; }

    public string? AvatarLink { get; set; }

    [Required]
    [MaxLength(255)]
    public string UserName { get; set; }

    [Required]
    public Role Role { get; set; }

    [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Must be a phone number")]
    public string? PhoneNumber { get; set; }

}