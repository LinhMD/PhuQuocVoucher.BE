﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Data.Models;


[Index(nameof(UserInfoId), IsUnique = true)]
public class ServiceProvider
{
    public int Id { get; set; }

    [Required]
    public string ProviderName { get; set; }

    public string? Address { get; set; }

    public int Status { get; set; }

    public string TaxCode { get; set; }

    public User UserInfo { get; set; }

    public int UserInfoId { get; set; }

    public Seller AssignedSeller { get; set; }

    public IEnumerable<Service> Services { get; set; }


    public ProviderType Type { get; set; }
}