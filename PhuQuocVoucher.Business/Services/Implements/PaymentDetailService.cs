using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class PaymentDetailService : ServiceCrud<PaymentDetail>, IPaymentDetailService
{
    public PaymentDetailService(IUnitOfWork work) : base(work.Get<PaymentDetail>(), work)
    {
    }
}