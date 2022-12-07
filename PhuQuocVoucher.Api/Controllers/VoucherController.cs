using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.Ultility;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Provider)}")]
[Route("api/v1/[controller]s")]
public class VoucherController : ControllerBase
{
    private readonly IVoucherService _voucherService;

    private readonly ILogger<VoucherController> _logger;

    private readonly IRepository<Voucher> _repo;

    private IUnitOfWork _work;

    public VoucherController(IVoucherService voucherService, ILogger<VoucherController> logger, IUnitOfWork work)
    {
        _voucherService = voucherService;
        _logger = logger;
        _work = work;
        _repo = work.Get<Voucher>();
    }

    [HttpGet("admin")]
    public async Task<IActionResult> Get([FromQuery] FindVoucher request, [FromQuery] PagingRequest paging, string? orderBy,[FromClaim("ProviderId")] int? providerId)
    {
        request.ProviderId = providerId ?? request.ProviderId;
        return Ok((await _voucherService.GetAsync<VoucherView>(new GetRequest<Voucher>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Voucher>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [AllowAnonymous]
    [HttpGet("{voucherId:int}/seller/{sellerId:int} ")]
    public async Task<RedirectResult> GetVoucher(int voucherId, int sellerId)
    {
        (await Task.FromResult("heloo")).Dump();
        //todo: add voucher M:M seller
        return Redirect($"https://phuquoc-voucher.vercel.app/voucher-detail/{voucherId}/seller/{sellerId}");
    }


    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetDefault([FromQuery] FindVoucher request, [FromQuery] PagingRequest paging, string? orderBy, [FromClaim("ProviderId")] int? providerId)
    {
        request.ProviderId = providerId ?? request.ProviderId;
        return Ok((await _voucherService.GetAsync<VoucherView>(new GetRequest<Voucher>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Voucher>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVoucher request, [FromClaim("ProviderId")] int? providerId)
    {
        request.ProviderId = providerId ?? request.ProviderId;
        return Ok(await _voucherService.CreateAsync(request));
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, [FromClaim("ProviderId")] int? providerId)
    {
        return Ok(await _repo.Find<VoucherView>(cus => cus.Id == id && (providerId == null || cus.ProviderId == providerId) ).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Voucher)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateVoucher request, int id, [FromClaim("ProviderId")] int? providerId)
    {
        
        var voucherIds = await _repo.Find(v => providerId == null ||  v.ProviderId == providerId).Select(v => v.Id).ToListAsync();
        if (voucherIds.Contains(id))
            return Ok(await _voucherService.UpdateAsync(id, request));
        
        return BadRequest();
    }
    [HttpPut("{id:int}/tag")]
    public async Task<IActionResult> UpdateTags([FromBody] IList<int> tags, int id, [FromClaim("ProviderId")] int? providerId)
    {
        var voucherIds = await _repo.Find(v => providerId == null || v.ProviderId == providerId).Select(v => v.Id).ToListAsync();
        if (voucherIds.Contains(id))
            return Ok(await _voucherService.UpdateTag(tags, id));
        
        return BadRequest();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, [FromClaim("ProviderId")] int? providerId)
    {
        var voucherIds = await _repo.Find(v => providerId == null || v.ProviderId == providerId).Select(v => v.Id).ToListAsync();
        if (voucherIds.Contains(id))
            return Ok(await _voucherService.DeleteAsync(id));
        return BadRequest();
    }
    
    [HttpPost("autogenerate")]
    public async Task<IActionResult> GenerateQr(GenerateQrcode voucherGen)
    {
        var vouchers = await _repo.Find(v => voucherGen.voucherIds.Contains(v.Id) && v.Status == ModelStatus.Active && !v.IsCombo).ToListAsync();

        var qrCodes = new List<QrCode>();
        foreach (var voucher in vouchers)
        {
            qrCodes.AddRange(Enumerable.Range(0, voucherGen.inventory).Select(qr => new QrCode()
            {
                QrCodeStatus = QrCodeStatus.Active,
                CreateAt = DateTime.Now,
                HashCode = Guid.NewGuid().ToString(),
                ProviderId = voucher.ProviderId,
                VoucherId = voucher.Id ,
                ServiceId = voucher.ServiceId ?? 0,
                Status = ModelStatus.Active,
                EndDate = voucher.EndDate ?? DateTime.Now,
                StartDate = voucher.StartDate ?? DateTime.Now
            }));
        }

        await _work.Get<QrCode>().AddAllAsync(qrCodes);
        await _voucherService.UpdateVoucherInventoryList(voucherGen.voucherIds);
        return Ok();
    }
    [HttpPut("status")]
    public async Task<IActionResult> UpdateStatus(UpdateVoucherStatus update)
    {
        var vouchers = await _repo.Find(v => update.voucherIds.Contains(v.Id) && v.Status == ModelStatus.Active && !v.IsCombo).ToListAsync();

        foreach (var voucher in vouchers)
        {
            voucher.Status = update.status;
        }

        await _work.CompleteAsync();
        return Ok();
    }
    
    public class UpdateVoucherStatus
    {
        public IList<int> voucherIds { get; set; }
        public ModelStatus status { get; set; }
    }
    
    public class GenerateQrcode
    {
        public IList<int> voucherIds { get; set; }
        public int inventory { get; set; }
    }
}