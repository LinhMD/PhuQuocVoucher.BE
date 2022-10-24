﻿using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.ProfileDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    private readonly ILogger<ProfileController> _logger;

    private readonly IRepository<Profile> _repo;

    public ProfileController(IProfileService profileService, ILogger<ProfileController> logger, IUnitOfWork work)
    {
        _profileService = profileService;
        _logger = logger;
        _repo = work.Get<Profile>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindProfile request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        request.Status = ModelStatus.Active;
        return Ok((await _profileService.GetAsync(new GetRequest<Profile>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Profile>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }
    
    [HttpGet("admin")]
    public async Task<IActionResult> GetAdmin([FromQuery] FindProfile request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _profileService.GetAsync(new GetRequest<Profile>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Profile>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProfile request)
    {
        return Ok(await _profileService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find(profile => profile.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Profile)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateProfile request, int id)
    {
        return Ok(await _profileService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _profileService.DeleteAsync(id));
    }
}