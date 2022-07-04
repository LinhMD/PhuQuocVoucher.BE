﻿using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.TagDto;

public class FindTag: IFindRequest<Tag>
{

    public int? Id { get; set; }

    [Contain]
    public string? Name { get; set; }
}