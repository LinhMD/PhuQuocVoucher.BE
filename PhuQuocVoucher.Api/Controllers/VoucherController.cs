﻿using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Dtos.VoucherDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/voucher")]
[CrudExceptionFilter]
public class VoucherController : ControllerBase
{
    private readonly IVoucherService _voucherService;

    private readonly ILogger<VoucherController> _logger;

    private readonly IRepository<Voucher> _repo;

    public VoucherController(IVoucherService voucherService, ILogger<VoucherController> logger, IUnitOfWork work)
    {
        _voucherService = voucherService;
        _logger = logger;
        _repo = work.Get<Voucher>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindVoucher request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _voucherService.GetAsync(new GetRequest<Voucher>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<Voucher>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVoucher request)
    {
        return Ok(await _voucherService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find(cus => cus.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Voucher)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateVoucher request, int id)
    {
        return Ok(await _voucherService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _voucherService.DeleteAsync(id));
    }
}