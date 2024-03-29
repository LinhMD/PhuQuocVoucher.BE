﻿using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.TagDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    private readonly ILogger<Tag> _logger;

    private readonly IRepository<Tag> _repo;

    public TagController(ITagService tagService, ILogger<Tag> logger, IUnitOfWork work)
    {
        _tagService = tagService;
        _logger = logger;
        _repo = work.Get<Tag>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindTag request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _tagService.GetAsync(new GetRequest<Tag>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Tag>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTag request)
    {
        return Ok(await _tagService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find(tag => tag.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Tag)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateTag request, int id)
    {
        return Ok(await _tagService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _tagService.DeleteAsync(id));
    }
}