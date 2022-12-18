using Aspose.Cells;
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
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class QrCodeController : ControllerBase
{
    private readonly IQrCodeService _qrCodeService;

    private readonly ILogger<QrCodeController> _logger;

    private readonly IRepository<QrCode> _repo;

    private readonly IVoucherService _voucherService;

    private IVoucherRepository _voucherRepository;

    private IUnitOfWork _work;

    public QrCodeController(IQrCodeService qrCodeService, ILogger<QrCodeController> logger, IUnitOfWork work, IVoucherService voucherService)
    {
        _qrCodeService = qrCodeService;
        _logger = logger;
        _work = work;
        _voucherService = voucherService;
        _repo = work.Get<QrCode>();
        _voucherRepository = (work.Get<Voucher>() as IVoucherRepository)!;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindQrCode request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _qrCodeService.GetAsync(new GetRequest<QrCode>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<QrCode>(),
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
                  throw new ModelNotFoundException($"Not Found {nameof(QrCode)} with id {id}"));
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _qrCodeService.DeleteAsync(id));
    }

    [HttpPost("voucher/{voucherId:int}/import")]
    public async Task<IActionResult> UploadQrCodes(IFormFile file, int voucherId )
    {
        var workBook = new Workbook(file.OpenReadStream());

        var voucher = await _work.Get<Voucher>().Find(v => v.Id == voucherId).FirstOrDefaultAsync();
        

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
        
        var qrCodeInfos = qrCode.Select(qr => new QrCode()
        {
            QrCodeStatus = QrCodeStatus.Active,
            CreateAt = DateTime.Now,
            HashCode = qr,
            ProviderId = voucher?.ProviderId ?? 0,
            VoucherId = voucherId,
            ServiceId = voucher?.ServiceId ?? 0,
            Status = ModelStatus.Active,
            EndDate = voucher?.EndDate ?? DateTime.Now,
            StartDate = voucher?.StartDate ?? DateTime.Now
        });
        
        await _repo.AddAllAsync(qrCodeInfos);
        
        await _voucherService.UpdateVoucherInventoryList(new List<int>{voucherId});
        return Ok();
    }
}