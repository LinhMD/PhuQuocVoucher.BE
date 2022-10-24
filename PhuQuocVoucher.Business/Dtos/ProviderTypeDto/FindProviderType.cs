using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProviderTypeDto;

public class FindProviderType : IFindRequest<ProviderType>
{
    [Contain]
    public string? Name { get; set; }
    
    public ModelStatus Status { get; set; }
}