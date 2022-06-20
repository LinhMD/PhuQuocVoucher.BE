using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.CustomerDto;

public class FindCustomer : IFindRequest<Customer>
{
    public int? Id { get; set; }

    [Contain]
    public string? CustomerName { get; set; }

    [Equal("UserInfoId")]
    public int? UserId { get; set; }

    [Contain(target:"UserInfo.Email")]
    public string? UserName { get; set; }

    [Contain(target:"UserInfo.Email")]
    public string? Email { get; set; }

    [Equal("UserInfo.Role")]
    public Role? Role { get; } = Data.Models.Role.Customer;

    [Contain(target:"UserInfo.PhoneNumber")]
    public string? PhoneNumber { get; set; }
}