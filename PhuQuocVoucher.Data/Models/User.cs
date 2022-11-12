using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(UserName), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class User : BaseModel
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string? FireBaseUid { get; set; }

    public string? AvatarLink { get; set; }

    [Required]
    [MaxLength(255)]
    public string UserName { get; set; }

    public string? Hash { get; set; }

    public string? Salt { get; set; }

    [Required]
    public Role Role { get; set; }

    [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Must be a phone number")]
    [MaxLength(10)]
    public string? PhoneNumber { get; set; }
    
    
    

}