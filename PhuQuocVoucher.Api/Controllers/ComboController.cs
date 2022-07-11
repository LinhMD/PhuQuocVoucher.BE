using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Business.Dtos.ComboDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/combo")]
[CrudExceptionFilter]
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
    public async Task<IActionResult> Get([FromQuery] FindCombo request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _comboService.GetAsync(new GetRequest<Combo>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<Combo>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCombo request)
    {
        return Ok(await _comboService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find(cus => cus.Id == id).FirstOrDefaultAsync() ??
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