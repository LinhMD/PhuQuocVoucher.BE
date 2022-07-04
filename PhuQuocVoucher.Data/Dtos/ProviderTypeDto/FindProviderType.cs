using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.ProviderTypeDto;

public class FindProviderType : IFindRequest<ProviderType>
{
    [Contain]
    public string? Name { get; set; }
}