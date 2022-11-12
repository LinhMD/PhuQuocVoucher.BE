using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IVoucherService : IServiceCrud<Voucher>
{
    public Task<VoucherView> CreateAsync(CreateVoucher createVoucher);

}