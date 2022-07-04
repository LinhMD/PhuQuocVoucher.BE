﻿using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.PlaceDto;

public class FindPlace : IFindRequest<Place>
{
    [Equal]
    public int? Id { get; set; }

    [Contain]
    public string? Name { get; set; }

    [Contain]
    public string? MapLocation { get; set; }

}