using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/priceBook")]
public class PriceBookController : ControllerBase
{
    private readonly IPriceBookService _priceBookService;

    private readonly ILogger<PriceBookController> _logger;

    private readonly IRepository<PriceBook> _repo;

    public PriceBookController(IPriceBookService priceBookService, ILogger<PriceBookController> logger, IUnitOfWork work)
    {
        _priceBookService = priceBookService;
        _logger = logger;
        _repo = work.Get<PriceBook>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindPriceBook request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _priceBookService.GetAsync(new GetRequest<PriceBook>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<PriceBook>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePriceBook request)
    {
        return Ok(await _priceBookService.CreateAsync(request));
    }
    
    [HttpPost("many")]
    public async Task<IActionResult> CreateMultiple([FromBody] CreateListPriceBook request)
    {
        return Ok(await _priceBookService.CreateManyAsync(request.PriceBooks, request.ProductId));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find(priceBook => priceBook.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(PriceBook)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdatePriceBook request, int id)
    {
        return Ok(await _priceBookService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _priceBookService.DeleteAsync(id));
    }
}