using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;

namespace PhuQuocVoucher.Business.Dtos.UserDto;

public class FindUser : IFindRequest<Data.Models.User>, IDto
{
    public int? Id { get; set; }

    [EmailAddress]
    [Contain]
    public string? Email { get; set; }

    [Contain]
    public string? FireBaseUid { get; set; }

    public int? Status { get; set; }

    [Contain]
    public string? AvatarLink { get; set; }

    [MaxLength(255)]
    [Contain]
    public string? UserName { get; set; }

    [Contain]
    public string? PhoneNumber { get; set; }

}