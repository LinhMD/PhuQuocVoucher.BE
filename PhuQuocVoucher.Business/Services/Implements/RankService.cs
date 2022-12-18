using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class RankService : ServiceCrud<SellerRank>, IRankService
{
    public RankService(IUnitOfWork work, ILogger<RankService> logger) : base(work.Get<SellerRank>(), work, logger)
    {
        
    }
}