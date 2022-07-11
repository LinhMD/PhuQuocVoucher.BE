using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/seller")]
[CrudExceptionFilter]
public class SellerController : ControllerBase
{
    private readonly ISellerService _sellerService;

    private readonly ILogger<SellerController> _logger;

    private readonly IRepository<Seller> _repo;

    public SellerController(ISellerService sellerService, ILogger<SellerController> logger, IUnitOfWork work)
    {
        _sellerService = sellerService;
        _logger = logger;
        _repo = work.Get<Seller>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindSeller request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _sellerService.GetAsync<SellerView>(new GetRequest<Seller>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<Seller>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSeller request)
    {
        return Ok(await _sellerService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find<SellerView>(seller => seller.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Seller)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateSeller request, int id)
    {
        return Ok(await _sellerService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _sellerService.DeleteAsync(id));
    }
}