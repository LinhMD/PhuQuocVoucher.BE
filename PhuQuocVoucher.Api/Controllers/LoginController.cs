using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using FirebaseAdmin.Auth;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;
using PhuQuocVoucher.Business.Dtos.CustomerDto;
using PhuQuocVoucher.Business.Dtos.LoginDto;
using PhuQuocVoucher.Business.Dtos.MailDto;
using PhuQuocVoucher.Business.Dtos.UserDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories;
using ServiceProvider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _config;

    private readonly PqUnitOfWork _work;

    private readonly IUserService _userService;
    private readonly IMailingService _mailing;
    
    private readonly ICartService _cartService;

    public LoginController(IConfiguration config, IUnitOfWork work, IUserService userService, IMailingService mailing, ICartService cartService)
    {
        _config = config;
        _userService = userService;
        _mailing = mailing;
        _cartService = cartService;
        _work = (PqUnitOfWork?) work ?? throw new RuntimeBinderException();
    }



    [HttpGet]
    public async Task<IActionResult> GetUser(string username)
    {
        var user = await _work.Users.Find(u => u.UserName == username && u.Status == ModelStatus.Active)
            .FirstOrDefaultAsync();
        
        return user == null ? BadRequest("User not found") : Ok(LoginHelper.GenerateJwt(user, _config, await GetAdditionalClaims(user)));
    }

    [NonAction]
    private async Task<IDictionary<string, string?>?> GetAdditionalClaims(User user)
    {
        var additionalClaims = default(Dictionary<string, string?>);
        switch (user.Role)
        {
            case Role.Customer:
            {
                var customer = await _work.Get<Customer>().Find(c => c.UserInfoId == user.Id).FirstOrDefaultAsync();
                var cartId = await _work.Get<Cart>().Find(c => c.CustomerId == customer.Id).Select(c => c.Id).FirstOrDefaultAsync();
                additionalClaims = new Dictionary<string, string?>
                {
                    {"CartId", cartId.ToString()},
                    {"CustomerId", customer?.Id.ToString()}
                };
                break;
            }
            case Role.Seller:
            {
                var seller = await _work.Get<Seller>().Find(c => c.UserInfoId == user.Id).FirstOrDefaultAsync();
                additionalClaims = new Dictionary<string, string?>
                {
                    {"SellerId", seller?.Id.ToString()}
                };
                break;
            }
            case Role.Admin:
                break;
            case Role.Provider:
            {
                var provider = await _work.Get<ServiceProvider>().Find(p => p.UserInfoId == user.Id).FirstOrDefaultAsync();
                
                additionalClaims = new Dictionary<string, string?>
                {
                    {"ProviderId", provider?.Id.ToString()}
                };
                break;
            }
            default:
                additionalClaims = null;
                break;
        }
        return additionalClaims;

    }

    [HttpPost("firebase")]
    public async Task<IActionResult> GetFireBaseUser([FromBody] string token)
    {
        var userRecord = await LoginHelper.GetFireBaseUserByToken(token);
        var uid = userRecord.Uid;
        var user = await _work.Users.Find(u => u.FireBaseUid == uid && u.Status == ModelStatus.Active).FirstOrDefaultAsync() ??
                   await SignUpFromFirebaseAsync(userRecord);
        return Ok(LoginHelper.GenerateJwt(user, _config, await GetAdditionalClaims(user)));
    }

    private async Task<User> SignUpFromFirebaseAsync(UserRecord userRecord)
    {
        var user = new User
        {
            FireBaseUid = userRecord.Uid,
            UserName = userRecord.DisplayName ?? userRecord.Email,
            Email = userRecord.Email,
            Role = Role.Customer,
            PhoneNumber = userRecord.PhoneNumber,
            Status = ModelStatus.Active
        };

        user.Validate();

        user = await _work.Users.AddAsync(user);

        var customer = new Customer()
        {
            UserInfo = user,
            UserInfoId = user.Id,
            CustomerName = user.UserName,
            CreateAt = DateTime.Now,
        }; 
        
        await _work.Get<Customer>().AddAsync(customer);

        var cart = await _cartService.GetCartByCustomerAsync(customer.Id);
        customer.CartId = cart.Id;
        await _work.CompleteAsync();
        return user;
    }


    [HttpPost("signup")]
    public async Task<IActionResult> SignUpUser(UserSignUp userSignUp)
    {
        var salt = LoginHelper.GenerateSalt();
        var hash = LoginHelper.ComputeHash(
                    Encoding.UTF8.GetBytes(userSignUp.Password),
                        Encoding.UTF8.GetBytes(salt));

        var userView = await _userService.SignUpAsync(userSignUp, hash, salt);
        return Ok(userView);
    }
    
    [HttpPost("signup/customer")]
    public async Task<IActionResult> SignUpCustomer(CustomerSignUp customerSignUp)
    {
        var salt = LoginHelper.GenerateSalt();
        var hash = LoginHelper.ComputeHash(
            Encoding.UTF8.GetBytes(customerSignUp.UserInfo.Password),
            Encoding.UTF8.GetBytes(salt));

        customerSignUp.UserInfo.Role = Role.Customer;
        
        var userView = await _userService.SignUpAsync(customerSignUp.UserInfo, hash, salt, ModelStatus.Disable);
        
        var customer = new Customer()
        {
            Status = ModelStatus.Active,
            UserInfoId = userView.Id,
            CustomerName = customerSignUp.CustomerName,
            CreateAt = DateTime.Now
        };
        
        await _work.Get<Customer>().AddAsync(customer);

        
        var cart = await _cartService.GetCartByCustomerAsync(customer.Id);

        customer.CartId = cart.Id;
        await _mailing.SendEmailAsync(new MailTemplateRequest
        {
            values = new Dictionary<string, string>
            {
                {"userid", userView.Id.ToString() ?? "0"},
                {"UserName", userView.UserName ??"There"}
            },
            MailRequest = new MailRequest
            {
                Subject = "Activate account",
                ToEmail = customerSignUp.UserInfo.Email,
            },
            FileTemplateName = "ActivateAccount"
        });

        await _work.CompleteAsync();
        return Ok();
    }

    [HttpGet("activate/{userId:int}")]
    public async Task<RedirectResult> ActivateAccount(int userId)
    {
        var user = await _work.Get<User>().Find(u => u.Id == userId).FirstOrDefaultAsync();
        if (user == null) return RedirectPermanent("https://www.google.com/maps");
        user.Status = ModelStatus.Active;
        await _work.CompleteAsync();
        return RedirectPermanent("https://phuquoc-voucher.vercel.app/login");
    }

    [HttpPost("forgot")]
    public async Task<IActionResult> ForgotPassword(string newPassword, [FromClaim("Id")] int? userId)
    {
        var salt = LoginHelper.GenerateSalt();
        var hash = LoginHelper.ComputeHash(
            Encoding.UTF8.GetBytes(newPassword),
            Encoding.UTF8.GetBytes(salt));

        var user = await _work.Users.Find(u => u.Id == userId && u.Status == ModelStatus.Active).FirstOrDefaultAsync();

        if (user == null) return BadRequest("User not found? why?");

        user.Hash = hash;
        user.Salt = salt;

        await _work.Users.CommitAsync();
        return Ok(user.Adapt<UserView>());
    }

    [HttpPost]
    public async Task<IActionResult> LoginWithPassword(LoginRequest request)
    {
        var repository = _work.Get<User>();
        var user = await repository.Find(user => user.Email == request.Email && user.Status == ModelStatus.Active).FirstOrDefaultAsync();
        if (user == null)
            return NotFound("User not found with email: " + request.Email);

        var hash = LoginHelper.ComputeHash(Encoding.UTF8.GetBytes(request.Password),
            Encoding.UTF8.GetBytes(user!.Salt ?? ""));
        if (hash != user.Hash)
            return Unauthorized("Wrong password");

        return Ok(LoginHelper.GenerateJwt(user, _config, await GetAdditionalClaims(user)));
    }

    [HttpPost("addMany")]
    public async Task<IActionResult> CreateMultipleUser(IList<SignUpRequest> list)
    {
        var repository = _work.Get<User>();
        await repository.AddAllAsync(list.Select(u => new User()
        {
            Email = u.Email,
            Role = u.Role,
            UserName = u.Email.Split("@")[0],
            Status = ModelStatus.Disable,
            CreateAt = DateTime.Now,
            
        }));

        var createdUsers = await repository.Find(user => list.Select(l => l.Email).Contains(user.Email)).ToListAsync();
        var providers = createdUsers.Where(u => u.Role == Role.Provider).Select(u => new ServiceProvider()
        {
            Status = ModelStatus.Active,
            UserInfoId = u.Id,
            ProviderName = u.UserName,
            CreateAt = DateTime.Now
        }).ToList();
        
        var sellers = createdUsers.Where(u => u.Role == Role.Seller).Select(u => new Seller()
        {
            Status = ModelStatus.Active,
            UserInfoId = u.Id,
            SellerName = u.UserName,
            CreateAt = DateTime.Now
        }).ToList();
        
        await _work.Get<Seller>().AddAllAsync(sellers);
        await _work.Get<ServiceProvider>().AddAllAsync(providers);
        
        createdUsers.Select(u => new MailTemplateRequest
        {
            values = new Dictionary<string, string>
            {
                {"UserName", u.UserName},
                {"Email", u.Email},
                {"jwt", LoginHelper.GenerateJwt(u, _config)}
            },
            MailRequest = new MailRequest
            {
                Subject = "Create Password",
                ToEmail = u.Email,
            },
            FileTemplateName = "CreatePassword"
        }).ToList().ForEach(m => _mailing.SendEmailAsync(m));
        return Ok();
    }

    private static class LoginHelper
    {
        public static string GenerateSalt()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        }

        public static string ComputeHash(byte[] password, byte[] salt)
        {
            var byteResult = new Rfc2898DeriveBytes(password, salt, 10000);
            return Convert.ToBase64String(byteResult.GetBytes(24));
        }


        public static string GenerateJwt(User user, IConfiguration configuration, IDictionary<string, string?>? additionalClaims = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSetting:SecurityKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            
            var claims = new List<Claim>()
            {
                new("Id", user.Id.ToString()),
                new(ClaimTypes.NameIdentifier,user.UserName),
                new(ClaimTypes.Role, user.Role.ToString()),
                new(ClaimTypes.Email, user.Email)
            };
            
            if (additionalClaims != null)
            {
                claims.AddRange(additionalClaims.Where(pair => pair.Value != null).Select(pair =>  new Claim(pair.Key, pair.Value!)));
            }

            var token = new JwtSecurityToken(configuration["JwtSetting:Issuer"],
                configuration["JwtSetting:Audience"],
                claims,
                expires: DateTime.Now.AddSeconds(double.Parse(configuration["JwtSetting:ExpiredSeconds"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public static async Task<UserRecord> GetFireBaseUserByToken(string token)
        {
            var decode = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            var uid = decode.Uid;
            return await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        }
    }


}