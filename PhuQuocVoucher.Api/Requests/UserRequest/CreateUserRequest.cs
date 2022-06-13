using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Requests.UserRequest;

public class CreateUserRequest: ICreateRequest<User>
{

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string? AvatarLink { get; set; }

    [Required]
    [MaxLength(255)]
    public string UserName { get; set; }

    public int Status => 1;

    [Required]
    public Role Role { get; set; }

    [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Must be a phone number")]
    public string? PhoneNumber { get; set; }

}