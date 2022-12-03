﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class VoucherCompaign : BaseModel, IOrderAble
{

    public int Id { get; set; }
    public string VoucherName { get; set; }

    public int? LimitPerDay { get; set; }

    public bool? IsRequireProfileInfo { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Service Service { get; set; }

    public int ServiceId { get; set; }

    public IList<Voucher> QrCodeInfos { get; set; }
    
    public int SlotNumber { get; set; }

    public int ProviderId { get; set; }

    public ServiceProvider Provider { get; set; }
    
    public string? Description { get; set; }

    public string? Summary { get; set; }

    public int Inventory { get; set; }
    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public IEnumerable<PriceBook> Prices { get; set; }
    
    public IEnumerable<TagVoucher> Tags { get; set; }

    public IEnumerable<Review> Reviews { get; set; }

    public void ConfigOrderBy()
    {
        SetUpOrderBy<VoucherCompaign>();
        Expression<Func<VoucherCompaign, DateTime?>> startDate = voucher => voucher.StartDate;
        OrderByProvider<VoucherCompaign>.OrderByDic.Add(nameof(StartDate), startDate);
        Expression<Func<VoucherCompaign, DateTime?>> endDate = voucher => voucher.EndDate;
        OrderByProvider<VoucherCompaign>.OrderByDic.Add(nameof(EndDate), endDate);
    }
}