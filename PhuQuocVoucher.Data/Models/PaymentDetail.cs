﻿using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class PaymentDetail  : BaseModel, IOrderAble
{
    public int Id { get; set; }

    public double? TotalAmount { get; set; }
    
    public Guid? RequestId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Content { get; set; }
    
    public Order? Order { get; set; }

    public int? OrderId { get; set; }

    public User User { get; set; }
    
    public int? UserId { get; set; }

    public PaymentStatus? PaymentStatus { get; set; }

    
    public bool? IsValid { get; set; } = true;
    public void ConfigOrderBy()
    {
        Expression<Func<PaymentDetail, PaymentStatus?>> orderByStatus = payment => payment.PaymentStatus;
        OrderByProvider<PaymentDetail>.OrderByDic.Add(nameof(PaymentStatus),orderByStatus);
    }
}