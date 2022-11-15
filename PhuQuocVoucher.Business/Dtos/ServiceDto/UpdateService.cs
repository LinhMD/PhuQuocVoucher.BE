﻿using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ServiceDto;

public class UpdateService : UpdateDto, IUpdateRequest<Service>
{
    public string? Name { get; set; }

    public string? Description { get; set; }
    
    public int? TypeId { get; set; }
    
    public int? ServiceLocationId { get; set; }
    
    public ModelStatus? Status { get; set; }
}