using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using CrudApiTemplate.OrderBy;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(Email), IsUnique = true)]
public class User : BaseModel, IOrderAble
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
    public void ConfigOrderBy()
    {
        SetUpOrderBy<User>();
    }
    
    

}