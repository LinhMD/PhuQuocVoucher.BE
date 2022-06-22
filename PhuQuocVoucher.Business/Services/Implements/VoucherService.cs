using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class VoucherService : ServiceCrud<Voucher>, IVoucherService
{
    public VoucherService(IUnitOfWork work) : base(work.Get<Voucher>(), work)
    {
    }
}