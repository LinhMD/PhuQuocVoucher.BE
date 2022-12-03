using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.UserDto;
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

    public async Task<UserView> SignUpAsync(UserSignUp signUp, string hash, string salt, ModelStatus status = ModelStatus.Active)
    {
        var user = new User
        {
            Email = signUp.Email,
            UserName = signUp.UserName,
            Role = signUp.Role,
            Hash = hash,
            Salt = salt,
            Status = status,
            PhoneNumber = signUp.PhoneNumber
        };

        user.Validate();

        try
        {
            user = await Repository.AddAsync(user);
            switch (user.Role)
            {
                case Role.Customer:
                {
                    var customer = new Customer()
                    {
                        Status = ModelStatus.Active,
                        CreateAt = DateTime.Now,
                        CustomerName = user.UserName,
                        UserInfoId = user.Id,
                    };
                    await UnitOfWork.Get<Customer>().AddAsync(customer);
                    break;
                }
                case Role.Seller: 
                {
                    var seller = new Seller()
                    {
                        Status = ModelStatus.Active,
                        CreateAt = DateTime.Now,
                        SellerName = user.UserName,
                        UserInfoId = user.Id,
                        UserInfo = user,
                        CommissionRate = 0.02f
                    };
                    await UnitOfWork.Get<Seller>().AddAsync(seller);
                    break;
                }
                case Role.Provider:
                {
                    var provider = new ServiceProvider()
                    {
                        Status = ModelStatus.Active,
                        CreateAt = DateTime.Now,
                        ProviderName = user.UserName,
                        UserInfoId = user.Id
                    };
                    await UnitOfWork.Get<ServiceProvider>().AddAsync(provider);
                    break;
                }
                case Role.Admin:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e.InnerException, e.InnerException?.StackTrace ?? e.StackTrace);
            var exceptionMessage = e.InnerException?.Message ?? "";

            if (exceptionMessage.Contains("duplicate"))
            {
                var dupValue = exceptionMessage.Substring(exceptionMessage.IndexOf('(') + 1,
                    (exceptionMessage.IndexOf(')') - exceptionMessage.IndexOf('(') - 1));
                throw new DbQueryException($"Duplicate value: {dupValue}", DbError.Create);
            }

            throw new DbQueryException($"Error create {nameof(User)}  with message: {exceptionMessage}.", DbError.Create);
        }

        var view = user.Adapt<UserView>();

        return view;
    }
}