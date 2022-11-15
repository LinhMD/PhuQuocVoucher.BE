﻿using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class Profile : BaseModel, IOrderAble
{
    public int Id { get; set; }

    public int? Sex { get; set; }

    [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Must be a phone number")]
    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Name { get; set; }

    public string? CivilIdentify { get; set; }

    public int? CustomerId { get; set; }

    public Customer? Customer { get; set; }
    public void ConfigOrderBy()
    {
        Expression<Func<Profile, ModelStatus>> orderByStatus = profile => profile.Status;
        OrderByProvider<Profile>.OrderByDic.Add(nameof(Status),orderByStatus);
    }
}