using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.ProfileDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IProfileService : IServiceCrud<Profile>
{
    public Task<IList<ProfileView>> GetProfileOfCustomer(int customerId);
}