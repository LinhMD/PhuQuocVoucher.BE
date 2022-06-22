using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;
using Provider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Api.Dtos.ProviderDto;

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

    [Contain]
    public string? Email { get; set; }

    [Contain]
    public string? PhoneNumber { get; set; }

    [Equal]
    public int? UserInfoId { get; set; }

    [Equal("AssignedSeller.Id")]
    public int? AssignedSellerId { get; set; }

    [Contain("Type.Name")]
    public string? TypeName { get; set; }

    [Equal("Type.Id")]
    public int? TypeId { get; set; }
}