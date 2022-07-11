using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class PaymentDetailService : ServiceCrud<PaymentDetail>, IPaymentDetailService
{
    private ILogger<PaymentDetailService> _logger;
    public PaymentDetailService(IUnitOfWork work, ILogger<PaymentDetailService> logger) : base(work.Get<PaymentDetail>(), work, logger)
    {
        _logger = logger;
    }
}