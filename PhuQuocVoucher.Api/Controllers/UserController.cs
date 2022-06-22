﻿using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using PhuQuocVoucher.Api.Dtos.UserDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
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
        return Ok(await _userService.GetAsync<UserView>(id));
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