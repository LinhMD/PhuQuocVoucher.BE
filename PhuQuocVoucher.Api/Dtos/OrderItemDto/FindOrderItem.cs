﻿using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.OrderItemDto;

public class FindOrderItem : IFindRequest<OrderItem>, IDto
{
    public int? Id { get; set;}

    public int? OrderId { get; set; }

    public int? OrderProductId { get; set; }

    public int? ProfileId { get; set; }

}