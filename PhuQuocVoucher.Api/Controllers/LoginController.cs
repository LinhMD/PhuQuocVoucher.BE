using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CrudApiTemplate.Repository;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Api.Ultility;
using PhuQuocVoucher.Business.Repositories;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/login")]
[ModelNotFoundExceptionFilter]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _config;

    private readonly PqUnitOfWork _work;


    public LoginController(IConfiguration config, IUnitOfWork work )
    {
        _config = config;
        _work = (PqUnitOfWork?) work ?? throw new InvalidOperationException();
    }

    [HttpPost("firebase")]
    public async Task<IActionResult> GetFireBaseUser([FromBody] string token)
    {
        var userRecord = await FirebaseUtility.GetFireBaseUserByToken(token);
        var uid = userRecord.Uid;
        var user = await _work.Users.Find(u => u.FireBaseUid == uid && u.Status == ModelStatus.Active).FirstOrDefaultAsync() ??
                   await SignUpFromFirebaseAsync(userRecord);
        return Ok(GenerateJwt(user));

    }

    [HttpGet]
    public async Task<IActionResult> GetUser( string username)
    {
        var user = await _work.Users.Find(u => u.UserName == username && u.Status == ModelStatus.Active)
            .FirstOrDefaultAsync();
        return user == null ? BadRequest() : Ok(GenerateJwt(user));
    }

    [HttpGet("random")]
    [Authorize(policy:"Admin")]
    public Task<IActionResult> Random()
    {
        return Task.FromResult<IActionResult>(Ok(new Random().NextInt64()));
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