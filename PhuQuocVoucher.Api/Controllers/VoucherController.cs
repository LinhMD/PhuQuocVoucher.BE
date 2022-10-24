using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
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

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindVoucher request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        request.Status = ModelStatus.Active;
        return Ok((await _voucherService.GetAsync<VoucherView>(new GetRequest<Voucher>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Voucher>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }
    
    [HttpGet("admin")]
    public async Task<IActionResult> GetAdmin([FromQuery] FindVoucher request, [FromQuery] PagingRequest paging, string? orderBy)
    {
    
    [HttpGet]
    public async Task<IActionResult> GetDefault([FromQuery] FindVoucher request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _voucherService.GetAsync<VoucherView>(new GetRequest<Voucher>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Voucher>(),
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
        return Ok(await _repo.Find<VoucherView>(cus => cus.Id == id).FirstOrDefaultAsync() ??
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