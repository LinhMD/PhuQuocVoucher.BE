using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

public class QrCodeRepository : Repository<QrCodeInfo>, IQrCodeRepository
{
    public QrCodeRepository(DbContext context) : base(context)
    {
    }
}