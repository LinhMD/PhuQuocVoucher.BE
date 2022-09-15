using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using Provider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Business.Dtos.ProviderDto;

public class FindProvider : IFindRequest<Provider>
{
    [Equal]
    public int? Id { get; set; }

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

    [Contain(target:"Type.Name")]
    public string? TypeName { get; set; }

    [Equal("Type.Id")]
    public int? TypeId { get; set; }
}