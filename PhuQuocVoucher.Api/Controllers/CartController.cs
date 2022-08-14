using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Business.Dtos.CartDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    private readonly ILogger<CartController> _logger;

    private readonly IRepository<Cart> _repo;

    public CartController(ICartService cartService, ILogger<CartController> logger, IUnitOfWork work)
    {
        _cartService = cartService;
        _logger = logger;
        _repo = work.Get<Cart>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindCart request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _cartService.GetAsync(new GetRequest<Cart>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<Cart>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCart request)
    {
        return Ok(await _cartService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find(cus => cus.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Cart)} with id {id}"));
    }


    [HttpDelete("{id:int}/cart")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _cartService.DeleteAsync(id));
    }
}