using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProfileDto;

public class CreateProfile : CreateDto, ICreateRequest<Profile>
{
    [Required]
    public int Sex { get; set; }

    [Required]
    [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Must be a phone number")]
    public string PhoneNumber { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public string CivilIdentify { get; set; }

    [Required]
    public int CustomerId { get; set; }

}