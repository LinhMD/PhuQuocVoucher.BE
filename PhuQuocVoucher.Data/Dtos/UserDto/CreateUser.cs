using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.UserDto;

public class CreateUser : CreateDto, ICreateRequest<User>
{

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string? AvatarLink { get; set; }

    [Required]
    [MaxLength(255)]
    public string UserName { get; set; }

    [Required]
    public Role Role { get; set; }

    [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Must be a phone number")]
    public string? PhoneNumber { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<CreateUser, User>.NewConfig();
    }
}