﻿using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class CartItemService : ServiceCrud<CartItem>, ICartItemService
{
    public CartItemService(IUnitOfWork work) : base(work.Get<CartItem>(), work)
    {
    }
}