using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProfileDto;

public class FindProfile : IFindRequest<Profile>
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

    public ModelStatus Status { get; set; }
}