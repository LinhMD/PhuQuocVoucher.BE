﻿using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CartItemDto;

public class FindCartItem : IFindRequest<CartItem>, IDto
{

    public int? Id { get; set; }

    public int? Quantity { get; set; }

    public int? VoucherId { get; set; }

    public int? CartId { get; set; }
}