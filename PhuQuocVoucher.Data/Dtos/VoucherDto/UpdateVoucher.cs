﻿using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.VoucherDto;

public class UpdateVoucher : UpdateDto, IUpdateRequest<Voucher>
{

    public string? VoucherName { get; set; }

    public double? Price { get; set; }

    public int? Inventory { get; set; }

    public int? LimitPerDay { get; set; }

    public bool? IsRequireProfileInfo { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

}