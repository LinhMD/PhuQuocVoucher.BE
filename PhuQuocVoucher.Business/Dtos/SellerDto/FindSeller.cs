using System.Linq.Expressions;
using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.SellerDto;

public class FindSeller : IFindRequest<Seller>
{
    public int? Id { get; set; }

    [Contain]
    public string? SellerName { get; set; }

    public int? UserInfoId { get; set; }

    public float? CommissionRate { get; set; }

    [Contain("UserInfo.PhoneNumber")]
    public string? PhoneNumber { get; set; }

    [Contain("UserInfo.Email")]
    public string? Email { get; set; }

    [Contain("UserInfo.UserName")]
    public string? UserName { get; set; }
    
}