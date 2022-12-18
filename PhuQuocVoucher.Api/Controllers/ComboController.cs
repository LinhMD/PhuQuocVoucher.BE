using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.ComboDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Authorize(Roles = $"{nameof(Role.Admin)}")]
[Route("api/v1/[controller]s")]
public class ComboController : ControllerBase
{
    
    private readonly IComboService _comboService;

    private readonly ILogger<ComboController> _logger;

    private readonly IRepository<Voucher> _repo;

    private readonly IKpiService _kpiService;
    public ComboController(IComboService comboService, ILogger<ComboController> logger, IUnitOfWork work, IKpiService kpiService)
    {
        _comboService = comboService;
        _logger = logger;
        _kpiService = kpiService;
        _repo = work.Get<Voucher>();
    }

    [HttpGet("admin")]
    public async Task<ActionResult<ComboView>> Get([FromQuery] FindCombo request, [FromQuery] PagingRequest paging, string? orderBy, DateTime? kpiStartDate, DateTime? kpiEndDate)
    {
        var (models, total) = await _comboService.GetAsync<ComboView>(new GetRequest<Voucher>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Voucher>(),
            PagingRequest = paging
        });
        var comboIds = models.Select(m => m.Id).ToList();
        var comboKpis = await _kpiService.GetVoucherKpi(comboIds, kpiStartDate, kpiEndDate);
        foreach (var combo in models)
        {
            comboKpis.TryGetValue(combo.Id , out var kpi);
            combo.Kpi = kpi ?? new VoucherKPI();
        }
        return Ok((models, total).ToPagingResponse(paging));
    }

    /*[AllowAnonymous]
    [HttpGet("/{voucherid:int}-{sellerId:int}/ ")]
    public async Task<RedirectResult> GetVoucher(int voucherId, int sellerId)
    {
        //todo: add voucher M:M seller
        return Redirect($"https://phuquoc-voucher.vercel.app/voucher-detail/{voucherId}/seller/{sellerId}");
    }*/


    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<ComboView>> GetDefault([FromQuery] FindCombo request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _comboService.GetAsync<ComboView>(new GetRequest<Voucher>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Voucher>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<ActionResult<ComboView>> Create([FromBody] CreateCombo request)
    {
        return Ok(await _comboService.CreateCombo(request));
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ComboView>> Get(int id)
    {
        return Ok(await _repo.Find<ComboView>(cus => cus.Id == id ).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Voucher)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ComboView>> Update([FromBody] UpdateVoucher request, int id)
    {
        return Ok(await _comboService.UpdateAsync(id, request));
    }
    
    [HttpPut("{id:int}/tag")]
    public async Task<ActionResult<ComboView>> UpdateTags([FromBody] IList<int> tags, int id)
    {
        return Ok(await _comboService.UpdateTag(tags, id));
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ComboView>> Delete(int id)
    {
        return Ok(await _comboService.DeleteAsync(id));
    }
    
}