﻿using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(ProductId), IsUnique = true)]
[Index(nameof(Name), IsUnique = true)]
public class Combo : BaseModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public double Price { get; set; }
    public Product? Product { get; set; }
    public int? ProductId { get; set; }

    public IEnumerable<Voucher> Vouchers { get; set; }
}