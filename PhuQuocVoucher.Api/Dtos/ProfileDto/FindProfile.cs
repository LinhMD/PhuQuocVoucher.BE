using CrudApiTemplate.Attributes.Search;

namespace PhuQuocVoucher.Api.Dtos.ProfileDto;

public class FindProfile
{
    public int? Sex { get; set; }

    [Contain]
    public string? PhoneNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [Contain]
    public string? Name { get; set; }

    [Contain]
    public string? CivilIdentify { get; set; }

    public int? CustomerId { get; set; }

}