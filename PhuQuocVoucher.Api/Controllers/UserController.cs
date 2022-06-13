using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using PhuQuocVoucher.Api.Requests.UserRequest;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetUser([FromQuery]FindUserRequest request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        return Ok(_userService.Get(new GetRequest<User>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<User>(),
            PagingRequest = paging
        }).models);
    }

    [HttpPost]
    public IActionResult CreateUser(CreateUserRequest request)
    {
        return Ok(_userService.Create(request));
    }
}