﻿using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;
using ServiceProvider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Api.Dtos.ServiceDto;

public class CreateService : CreateDto, ICreateRequest<Service>
{

    [Required]
    [MaxLength(2048)]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public int TypeId { get; set; }

    [Required]
    public int ServiceLocationId { get; set; }

    [Required]
    public int ProviderId { get; set; }
}