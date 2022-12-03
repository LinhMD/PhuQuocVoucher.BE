﻿using Aspose.Cells;
using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Authorization;
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

    private readonly IRepository<Voucher> _repo;

    private readonly IVoucherService _voucherService;

    public QrCodeController(IQrCodeService qrCodeService, ILogger<QrCodeController> logger, IUnitOfWork work, IVoucherService voucherService)
    {
        _qrCodeService = qrCodeService;
        _logger = logger;
        _voucherService = voucherService;
        _repo = work.Get<Voucher>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindQrCode request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _qrCodeService.GetAsync(new GetRequest<Voucher>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<Voucher>(),
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
                  throw new ModelNotFoundException($"Not Found {nameof(Voucher)} with id {id}"));
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

    [Authorize(Roles = nameof(Role.Provider))]
    [HttpPost("voucher/{voucherId:int}/import")]
    public async Task<IActionResult> UploadQrCodes(IFormFile file, int voucherId, [FromClaim("ProviderId")] int? providerId)
    {
        var workBook = new Workbook(file.OpenReadStream());

        var workSheet = workBook.Worksheets[0];
        var codes = workSheet.Cells.MaxDataRow;
        var qrCode = new List<string>();
        for (var i = 0; i < codes; i++)
        {
            qrCode.Add(workSheet.Cells[i, 0].Value.ToString() ?? string.Empty);
        }

        var duplicateQrCode = await _repo.Find(qr => qrCode.Contains(qr.HashCode)).ToListAsync();

        if (duplicateQrCode.Count != 0)
        {
            return BadRequest(duplicateQrCode);
        }
        
        var qrCodeInfos = qrCode.Select(qr => new Voucher()
        {
            QrStatus = VoucherStatus.Active,
            VoucherId = voucherId, 
            CreateAt = DateTime.Now,
            HashCode = qr,
            ProviderId = providerId
        });
        
        await _repo.AddAllAsync(qrCodeInfos);
        
        await _voucherService.UpdateInventory();
        return Ok();
    }
}