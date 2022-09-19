using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Business.Dtos.PriceLevelDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/priceLevel")]
public class PriceLevelController : ControllerBase
{
    private readonly IPriceLevelService _priceLevelService;

    private readonly ILogger<PriceLevelController> _logger;

    private readonly IRepository<PriceLevel> _repo;

    public PriceLevelController(IPriceLevelService priceLevelService, ILogger<PriceLevelController> logger, IUnitOfWork work)
    {
        _priceLevelService = priceLevelService;
        _logger = logger;
        _repo = work.Get<PriceLevel>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindPriceLevel request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _priceLevelService.GetAsync(new GetRequest<PriceLevel>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<PriceLevel>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePriceLevel request)
    {
        return Ok(await _priceLevelService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find(priceLevel => priceLevel.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(PriceLevel)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdatePriceLevel request, int id)
    {
        return Ok(await _priceLevelService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _priceLevelService.DeleteAsync(id));
    }
}