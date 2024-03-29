﻿using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.QrCodeDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/qrcode")]
public class QrCodeController : ControllerBase
{
    private readonly IQrCodeService _qrCodeService;

    private readonly ILogger<QrCodeController> _logger;

    private readonly IRepository<QrCodeInfo> _repo;

    public QrCodeController(IQrCodeService qrCodeService, ILogger<QrCodeController> logger, IUnitOfWork work)
    {
        _qrCodeService = qrCodeService;
        _logger = logger;
        _repo = work.Get<QrCodeInfo>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindQrCode request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _qrCodeService.GetAsync(new GetRequest<QrCodeInfo>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<QrCodeInfo>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQrCode request)
    {
        return Ok(await _qrCodeService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find(qrCode => qrCode.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(QrCodeInfo)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateQrCode request, int id)
    {
        return Ok(await _qrCodeService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _qrCodeService.DeleteAsync(id));
    }
}