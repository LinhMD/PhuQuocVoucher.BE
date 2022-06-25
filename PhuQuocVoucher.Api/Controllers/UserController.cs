using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.Dtos.UserDto;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/user")]
[CrudExceptionFilter]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    private readonly IRepository<User> _repository;

    public UserController(IUserService userService, IUnitOfWork work)
    {
        _userService = userService;
        _repository = work.Get<User>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]FindUser request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        return Ok((await _userService.GetAsync<UserView>(new GetRequest<User>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<User>(),
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
    public async Task<IActionResult> Update([FromBody] UpdateUser request, int id)
    {

        return Ok(await _userService.UpdateAsync(id, request));
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {

        return Ok(await _userService.DeleteAsync(id));
    }
}