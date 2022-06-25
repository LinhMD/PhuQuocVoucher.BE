using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

public class PaymentDetailRepository : Repository<PaymentDetail> , IPaymentDetailRepository
{
    public PaymentDetailRepository(DbContext context) : base(context)
    {
    }

    public override IQueryable<PaymentDetail> IncludeAll()
    {
        return Models.Include(p => p.Order);
    }
}