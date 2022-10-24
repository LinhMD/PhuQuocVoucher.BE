using System.Linq.Expressions;
using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.SellerDto;

public class FindSeller : IFindRequest<Seller>
{
    [In(target:"Id")]
    public IList<int>? Ids { get; set; }

    [Contain]
    public string? SellerName { get; set; }

    public int? UserInfoId { get; set; }

    public float? CommissionRate { get; set; }

    [Contain(target:"UserInfo.PhoneNumber")]
    public string? PhoneNumber { get; set; }

    [Contain(target:"UserInfo.Email")]
    public string? Email { get; set; }

    [Contain(target:"UserInfo.UserName")]
    public string? UserName { get; set; }
    
    public ModelStatus Status { get; set; }
    
}