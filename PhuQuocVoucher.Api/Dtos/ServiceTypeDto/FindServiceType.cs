using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.ServiceTypeDto;

public class FindServiceType : IFindRequest<ServiceType>
{
    public int? Id { get; set; }

    [Contain]
    public string? Name { get; set; }

    public int? ParentTypeId { get; set; }
}