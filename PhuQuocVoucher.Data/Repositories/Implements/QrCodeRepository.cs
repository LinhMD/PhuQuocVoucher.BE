using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class QrCodeRepository : Repository<QrCodeInfo>, IQrCodeRepository
{
    public QrCodeRepository(DbContext context) : base(context)
    {
    }
}