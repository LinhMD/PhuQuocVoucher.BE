﻿using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;
using ServiceProvider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Api.Dtos.ServiceDto;

public class FindService : IFindRequest<Service> , IDto
{
    public int? Id { get; set; }

    [Contain]
    public string? Name { get; set; }

    [Contain]
    public string? Description { get; set; }

    [Contain("Type.Id")]
    public int? TypeId { get; set; }

    [Contain("Type.Name")]
    public string? TypeName { get; set; }


    [Equal("ServiceLocation.Id")]
    public int? PlaceId { get; set; }

    [Contain("ServiceLocation.Name")]
    public string? PlaceName { get; set; }


    [Equal("Provider.Id")]
    public int? ProviderId { get; set; }

    [Contain("Provider.Name")]
    public string? ProviderName { get; set; }
}