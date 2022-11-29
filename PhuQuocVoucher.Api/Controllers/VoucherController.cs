using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public VoucherController(IVoucherService voucherService, ILogger<VoucherController> logger, IUnitOfWork work)
    {
        _voucherService = voucherService;
        _logger = logger;
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
        
        return Challenge();
    }
    [HttpPut("{id:int}/tag")]
    public async Task<IActionResult> UpdateTags([FromBody] IList<int> tags, int id, [FromClaim("ProviderId")] int? providerId)
    {
        var voucherIds = await _repo.Find(v => providerId == null || v.ProviderId == providerId).Select(v => v.Id).ToListAsync();
        if (voucherIds.Contains(id))
            return Ok(await _voucherService.UpdateTag(tags, id));
        
        return Challenge();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, [FromClaim("ProviderId")] int? providerId)
    {
        var voucherIds = await _repo.Find(v => providerId == null || v.ProviderId == providerId).Select(v => v.Id).ToListAsync();
        if (voucherIds.Contains(id))
            return Ok(await _voucherService.DeleteAsync(id));
        return Challenge();
    }
    
}