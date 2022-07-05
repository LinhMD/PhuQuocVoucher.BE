﻿using CrudApiTemplate.Services;
using PhuQuocVoucher.Data.Dtos.VoucherDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IVoucherService : IServiceCrud<Voucher>
{
    public Task<Voucher> CreateAsync(CreateVoucher createVoucher);

}