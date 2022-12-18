using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.CartDto;
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
            PhoneNumber = signUp.PhoneNumber,
            AvatarLink = signUp.AvatarLink
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
                    var cart = (await UnitOfWork.Get<Cart>().AddAsync(new Cart
                               {
                                   Status = ModelStatus.Active,
                                   CreateAt = DateTime.Now,
                                   CustomerId = customer.Id
                               })).Adapt<CartView>();
                    customer.CartId = cart.Id;
                    await UnitOfWork.CompleteAsync();
                    break;
                }
                case Role.Seller: 
                {
                    var rank = await UnitOfWork.Get<SellerRank>().Find(s => true).OrderBy(o => o.EpxRequired).FirstOrDefaultAsync();
                    if (rank == null) throw new ModelNotFoundException("seller rank missing from database");
                    var seller = new Seller()
                    {
                        Status = ModelStatus.Active,
                        CreateAt = DateTime.Now,
                        SellerName = user.UserName,
                        UserInfoId = user.Id,
                        UserInfo = user,
                        Rank = rank,
                        RankId = rank.Id,
                        CommissionRate = rank.CommissionRatePercent
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

    
    public async Task<User> UpdateUserAdmin(AdminUpdate update, int userId)
    {
        var user = await UnitOfWork.Get<User>().Find(u => u.Id == userId).FirstOrDefaultAsync();

        var provider = await UnitOfWork.Get<ServiceProvider>().Find((p => p.UserInfoId == userId)).FirstOrDefaultAsync();
        
        if (user == null)
        {
            throw new ModelNotFoundException($"Not found User with id {userId}");
        }

        if (update.UserName != null)
        {
            user.UserName = update.UserName;
        }
        
        if (update.Email != null)
        {
            user.Email = update.Email;
        }
        if (update.AvatarLink != null)
        {
            user.AvatarLink = update.AvatarLink;
        }
        if (update.PhoneNumber != null)
        {
            user.PhoneNumber = update.PhoneNumber;
        }
        if (update.Status != null)
        {
            user.Status = update.Status ?? ModelStatus.Active;
        }

        if (provider == null)
        {
            await UnitOfWork.CompleteAsync();
            return user;
        }
        
        if (update.TaxCode != null)
        {
            provider.TaxCode = update.TaxCode;
        }
        if (update.Address != null)
        {
            provider.Address = update.Address;
        }
        if (update.ProviderName != null)
        {
            provider.ProviderName = update.ProviderName;
        }
        await UnitOfWork.CompleteAsync();

        return user;
    }
}