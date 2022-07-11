using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProfileDto;

public class UpdateProfile : UpdateDto, IUpdateRequest<Profile>
{

    public int? Sex { get; set; }

    [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Must be a phone number")]
    public string? PhoneNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [MaxLength(255)]
    public string? Name { get; set; }

    public string? CivilIdentify { get; set; }

    public int? CustomerId { get; set; }
}