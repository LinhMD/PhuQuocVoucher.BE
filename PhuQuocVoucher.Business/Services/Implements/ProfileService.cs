using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ProfileService : ServiceCrud<Profile>, IProfileService
{
    public ProfileService(IUnitOfWork work) : base(work.Get<Profile>(), work)
    {
    }
}