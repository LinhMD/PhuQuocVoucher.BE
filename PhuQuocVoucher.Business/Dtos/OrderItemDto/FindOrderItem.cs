﻿using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderItemDto;

public class FindOrderItem : IFindRequest<OrderItem>, IDto
{
    public int? Id { get; set;}

    public int? OrderId { get; set; }

    public int? VoucherId { get; set; }

    public int? ProfileId { get; set; }

}