using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.UserDto;

public class FindUser : IFindRequest<User>, IDto
{
    [In(target:"Id")]
    public IList<int>? Ids { get; set; }

    [In(target:"Email")]
    public IList<string>? Emails { get; set; }

    [In(target:"PhoneNumber")]
    public IList<string>? PhoneNumbers { get; set; }

    [Equal]
    public ModelStatus? Status { get; set; }

    [Equal]
    public Role? Role { get; set; }

    [MaxLength(255)]
    [Equal]
    public string? UserName { get; set; }
    
}