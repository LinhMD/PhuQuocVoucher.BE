﻿using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.PriceBookDto;

public class CreatePriceBook : CreateDto, ICreateRequest<PriceBook>
{

    [Required]
    public PriceLevel PriceLevel { get; set; }

    [Required]
    public int VoucherId { get; set; }
    
    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Only positive number allowed")]
    public double Price { get; set; }
}