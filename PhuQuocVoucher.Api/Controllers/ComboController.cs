using System.Security.Claims;
using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.ComboDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class ComboController : ControllerBase
{
    private readonly IComboService _comboService;

    private readonly ILogger<ComboController> _logger;

    private readonly IRepository<Combo> _repo;

    public ComboController(IComboService comboService, ILogger<ComboController> logger, IUnitOfWork work)
    {
        _comboService = comboService;
        _logger = logger;
        _repo = work.Get<Combo>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindCombo request, 
        [FromQuery] PagingRequest paging, 
        string? orderBy, 
        [FromClaim(ClaimTypes.Role)] string? role)
    {
        request.Status = ModelStatus.Active;
        return Ok((await _comboService.FindComboAsync(new GetRequest<Combo>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Combo>(),
            PagingRequest = paging
        }, role)).ToPagingResponse(paging));
    }
    
    [HttpGet("admin")]
    public async Task<IActionResult> GetAdmin([FromQuery] FindCombo request, 
        [FromQuery] PagingRequest paging, 
        string? orderBy, 
        [FromClaim(ClaimTypes.Role)] string? role)
    {
        return Ok((await _comboService.FindComboAsync(new GetRequest<Combo>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Combo>(),
            PagingRequest = paging
        }, role)).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCombo request)
    {
        return Ok((await _comboService.CreateAsync(request)).Adapt<ComboView>());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find<ComboView>(cus => cus.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Combo)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateCombo request, int id)
    {
        return Ok(await _comboService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _comboService.DeleteAsync(id));
    }
}