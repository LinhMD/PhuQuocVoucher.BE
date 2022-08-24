using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Business.Dtos.ProductDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]s")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    private readonly ILogger<BlogController> _logger;

    private readonly IRepository<Product> _repo;

    private readonly IUnitOfWork _work;

    public ProductController(IProductService productService, ILogger<BlogController> logger, IUnitOfWork work)
    {
        _productService = productService;
        _logger = logger;
        _repo = work.Get<Product>();
        _work = work;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]FindProduct request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        return Ok((await _productService.GetAsync(new GetRequest<Product>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<Product>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateProduct request)
    {
        return Ok(await _productService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find(cus => cus.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Product)} with id {id}"));
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
}