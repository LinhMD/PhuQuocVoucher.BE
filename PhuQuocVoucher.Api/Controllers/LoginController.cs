using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhuQuocVoucher.Api.Ultility;
using PhuQuocVoucher.Business.Repositories;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _config;

    private readonly PqUnitOfWork _work;


    public LoginController(IConfiguration config, PqUnitOfWork work )
    {
        _config = config;
        _work = work;
    }

    [HttpPost("firebase")]
    public async Task<IActionResult> GetFireBaseUser([FromBody] string token)
    {
        var userRecord = await FirebaseUtility.GetFireBaseUserByToken(token);
        var uid = userRecord.Uid;
        var user = await _work.Users.Find(u => u.FireBaseUid == uid && u.Status == ModelStatus.Active).FirstOrDefaultAsync() ??
                   await SignUpAsync(userRecord);
        return Ok(GenerateJwt(user));

    }

    private async Task<User> SignUpAsync(UserRecord userRecord)
    {
        var user = new User()
        {
            FireBaseUid = userRecord.Uid,
            UserName = userRecord.DisplayName ?? userRecord.Email,
            Email = userRecord.Email,
            Role = Role.Customer,
            PhoneNumber = userRecord.PhoneNumber,
            Status = ModelStatus.Active
        };
        user = await _work.Users.AddAsync(user);
        return user;
    }

    private string GenerateJwt(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSetting:SecurityKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Username", user.UserName),
            new Claim("Role", user.Role.ToString())
        };
        var token = new JwtSecurityToken(_config["JwtSetting:Issuer"],
            _config["JwtSetting:Audience"],
            claims,
            expires: DateTime.Now.AddSeconds(double.Parse(_config["JwtSetting:ExpiredSeconds"])),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}