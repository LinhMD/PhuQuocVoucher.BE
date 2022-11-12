using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

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