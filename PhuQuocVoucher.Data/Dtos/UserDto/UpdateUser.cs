using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.UserDto;

public class UpdateUser : UpdateDto, IUpdateRequest<User>
{
    [EmailAddress]
    public string? Email { get; set; }

    public string? AvatarLink { get; set; }

    [MaxLength(255)]
    public string? UserName { get; set; }

    public Role? Role { get; set; }

    [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Must be a phone number")]
    [MaxLength(10)]
    public string? PhoneNumber { get; set; }

    public ModelStatus? Status { get; set; }


}