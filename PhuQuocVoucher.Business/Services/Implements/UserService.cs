using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class UserService : ServiceCrud<User>, IUserService
{
    public UserService(IUnitOfWork work) : base(work.Get<User>(), work)
    {
    }
}