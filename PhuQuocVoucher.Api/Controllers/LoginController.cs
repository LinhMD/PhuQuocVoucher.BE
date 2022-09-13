﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using FirebaseAdmin.Auth;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhuQuocVoucher.Api.CustomBinding;
using PhuQuocVoucher.Business.Dtos.LoginDto;
using PhuQuocVoucher.Business.Dtos.MailDto;
using PhuQuocVoucher.Business.Dtos.UserDto;
using PhuQuocVoucher.Business.Repositories;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _config;

    private readonly PqUnitOfWork _work;

    private readonly IUserService _userService;
    private readonly IMailingService _mailing;

    public LoginController(IConfiguration config, IUnitOfWork work, IUserService userService, IMailingService mailing)
    {
        _config = config;
        _userService = userService;
        _mailing = mailing;
        _work = (PqUnitOfWork?) work ?? throw new RuntimeBinderException();
    }



    [HttpGet]
    public async Task<IActionResult> GetUser(string username)
    {
        var user = await _work.Users.Find(u => u.UserName == username && u.Status == ModelStatus.Active)
            .FirstOrDefaultAsync();
        return user == null ? BadRequest("User not found") : Ok(LoginHelper.GenerateJwt(user, _config));
    }


    [HttpPost("firebase")]
    public async Task<IActionResult> GetFireBaseUser([FromBody] string token)
    {
        var userRecord = await LoginHelper.GetFireBaseUserByToken(token);
        var uid = userRecord.Uid;
        var user = await _work.Users.Find(u => u.FireBaseUid == uid && u.Status == ModelStatus.Active).FirstOrDefaultAsync() ??
                   await SignUpFromFirebaseAsync(userRecord);
        return Ok(LoginHelper.GenerateJwt(user, _config));
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
        var user = await repository.Find(user => user.Email == request.Email).FirstOrDefaultAsync();
        if (user == null)
            return NotFound("User not found with email: " + request.Email);

        var hash = LoginHelper.ComputeHash(Encoding.UTF8.GetBytes(request.Password),
            Encoding.UTF8.GetBytes(user!.Salt ?? ""));
        if (hash != user.Hash)
            return Unauthorized("Wrong password");

        return Ok(LoginHelper.GenerateJwt(user, _config));
    }

    [HttpPost("addMany")]
    public async Task<IActionResult> CreateMultipleUser(IList<SignUpRequest> list)
    {
        var repository = _work.Get<User>();
        await repository.AddAllAsync(list.Select(u => (u as ICreateRequest<User>).CreateNew(_work)));

        var createdUsers = await repository.Find(user => list.Select(l => l.Email).Contains(user.Email)).ToListAsync();

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

    public static class LoginHelper
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


        public static string GenerateJwt(User user, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSetting:SecurityKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Username", user.UserName),
                new Claim("Role", user.Role.ToString()),
                new Claim("Email", user.Email)
            };

            var token = new JwtSecurityToken(configuration["JwtSetting:Issuer"],
                configuration["JwtSetting:Audience"],
                claims,
                expires: DateTime.Now.AddSeconds(double.Parse(configuration["JwtSetting:ExpiredSeconds"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static async Task<UserRecord> GetFireBaseUser(string Uid)
        {
            var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(Uid);
            return userRecord;
        }

        public static async Task<UserRecord> GetFireBaseUserByToken(string token)
        {
            var decode = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            var uid = decode.Uid;
            return await GetFireBaseUser(uid);
        }
    }


}