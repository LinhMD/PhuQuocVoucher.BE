using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class UserService : ServiceCrud<User>, IUserService
{
    private ILogger<UserService> _logger;
    public UserService(IUnitOfWork work, ILogger<UserService> logger) : base(work.Get<User>(), work, logger)
    {
        _logger = logger;
    }
}