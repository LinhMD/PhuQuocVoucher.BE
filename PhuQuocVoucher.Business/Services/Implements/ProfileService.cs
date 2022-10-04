using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.ProfileDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ProfileService : ServiceCrud<Profile>, IProfileService
{
    private ILogger<ProfileService> _logger;
    public ProfileService(IUnitOfWork work, ILogger<ProfileService> logger) : base(work.Get<Profile>(), work, logger)
    {
        _logger = logger;
    }

    public async Task<IList<ProfileView>> GetProfileOfCustomer(int customerId)
    {
        return await Repository.Find<ProfileView>(profile => profile.CustomerId == customerId).ToListAsync();
    }
}