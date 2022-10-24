﻿using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CustomerDto;

public class FindCustomer : IFindRequest<Customer>
{
    public int? Id { get; set; }

    [Contain]
    public string? CustomerName { get; set; }

    [Equal("UserInfoId")]
    public int? UserId { get; set; }

    [Contain(target:"UserInfo.UserName")]
    public string? UserName { get; set; }

    [Contain(target:"UserInfo.Email")]
    public string? Email { get; set; }

    [Contain(target:"UserInfo.PhoneNumber")]
    public string? PhoneNumber { get; set; }

    [Equal(nameof(Customer.AssignSellerId))]
    public int? SellerId { get; set; }
    
    public ModelStatus Status { get; set; }

}