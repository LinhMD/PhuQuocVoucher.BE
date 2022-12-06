using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.UserDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;
using static PhuQuocVoucher.Api.Ultility.Common;
using ServiceProvider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    private readonly IRepository<User> _repository;

    private IUnitOfWork _work;

    public UserController(IUserService userService, IUnitOfWork work)
    {
        _userService = userService;
        _work = work;
        _repository = work.Get<User>();
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery]FindUser request,
        [FromQuery]PagingRequest paging,
        [RegularExpression(SortByRegexString)] string? orderBy)
    {
        return Ok((await _userService.GetAsync<UserView>(new GetRequest<User>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<User>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repository.Find<UserView>(cus => cus.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Data.Models.User)} with id {id}"));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUser request)
    {
        return Ok(await _userService.CreateAsync(request));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] AdminUpdate request, int id)
    {
        return Ok(await _userService.UpdateAsync(id, request));
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _userService.DeleteAsync(id));
    }

    [HttpGet("current")]
    [Authorize]
    public async Task<IActionResult> Current([FromClaim("Id")]int? id)
    {
        var user = await _repository.Find<UserView>(cus => cus.Id == id).FirstOrDefaultAsync() ??
                                  throw new ModelNotFoundException($"Not Found {nameof(Data.Models.User)} with id {id}");
        if (user.Role == Role.Provider)
        {
            var proID = await _work.Get<ServiceProvider>().Find(p => p.UserInfoId == user.Id).Select(p => p.Id).FirstOrDefaultAsync();
            user.ProviderId = proID;
        }
        return Ok(user);
    }
}