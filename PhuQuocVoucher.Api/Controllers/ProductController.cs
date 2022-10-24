using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.ProductDto;
using PhuQuocVoucher.Business.Dtos.TagDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]s")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    private readonly ILogger<ProductController> _logger;

    private readonly IRepository<Product> _repo;

    private readonly IUnitOfWork _work;

    public ProductController(IProductService productService, ILogger<ProductController> logger, IUnitOfWork work)
    {
        _productService = productService;
        _logger = logger;
        _repo = work.Get<Product>();
        _work = work;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]FindProduct request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        request.Status = ModelStatus.Active;
        return Ok((await _productService.GetAsync<ProductView>(new GetRequest<Product>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Product>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpGet("admin")]
    public async Task<IActionResult> GetAdmin([FromQuery]FindProduct request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        return Ok((await _productService.GetAsync<ProductView>(new GetRequest<Product>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Product>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateProduct request)
    {
        return Ok(await _productService.CreateProductAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find<ProductView>(pro => pro.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Product)} with id {id}"));
    }

    [HttpPost("{id:int}/tags")]
    public async Task<IActionResult> AddTag(IList<string> tags, int id)
    {
        return Ok(await _productService.AddTagsAsync(tags, id));

    } 

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateProduct request, int id)
    {
        return Ok(await _productService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _productService.DeleteAsync(id));
    }

    [HttpPost("inventory")]
    public async Task<ActionResult<IList<ProductInventoryView>>> GetInventory(IList<int> productIds)
    {

        return Ok(await _work.Get<Product>().Find(c => productIds.Contains(c.Id)).Select(c => new ProductInventoryView
        {
            Id = c.Id,
            Inventory = c.Inventory
        }).ToListAsync());
    }
}