using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;
using Provider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Business.Dtos.ProviderDto;

public class FindProvider : IFindRequest<Provider>
{
    [Equal]
    public int? Id { get; set; }

    [In(target:"Id")]
    public IList<int>? Ids { get; set; }
    
    [In(target:"UserInfo.Id")]
    public IList<int>? UserIds { get; set; }

    [In(target:"UserInfo.Email")]
    public IList<string>? Emails { get; set; }

    [In(target:"UserInfo.PhoneNumber")]
    public IList<string>? PhoneNumbers { get; set; }
    
    [Contain]
    public string? ProviderName { get; set; }

    [Contain]
    public string? Address { get; set; }

    [Contain]
    public string? TaxCode { get; set; }

    [Contain(target:"UserInfo.Email")]
    public string? Email { get; set; }

    [Contain(target:"UserInfo.PhoneNumber")]
    public string? PhoneNumber { get; set; }

    [Equal]
    public int? UserInfoId { get; set; }

    [Equal("AssignedSeller.Id")]
    public int? AssignedSellerId { get; set; }

    [BiggerThan(nameof(BaseModel.UpdateAt))]
    public DateTime? UpdateAt_startTime { get; set; }
    
    [LessThan(nameof(BaseModel.UpdateAt))]
    public DateTime? UpdateAt_endTime { get; set; }

    [BiggerThan(nameof(BaseModel.CreateAt))]
    public DateTime? CreateAt_startTime { get; set; }
    
    [LessThan(nameof(BaseModel.CreateAt))]
    public DateTime? CrateAt_endTime { get; set; }

    public ModelStatus? Status { get; set; }
}