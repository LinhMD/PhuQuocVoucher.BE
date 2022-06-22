using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class SellerService : ServiceCrud<Seller>, ISellerService
{
    public SellerService( IUnitOfWork work) : base(work.Get<Seller>(), work)
    {
    }
}