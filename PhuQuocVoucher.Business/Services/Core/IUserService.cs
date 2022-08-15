using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.UserDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IUserService : IServiceCrud<User>
{
    public Task<UserView> SignUpAsync(UserSignUp signUp, string hash, string salt);
}