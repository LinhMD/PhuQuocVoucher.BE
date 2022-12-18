using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.BlogDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]s")]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;

    private readonly ILogger<BlogController> _logger;

    private readonly IRepository<Blog> _repo;

    public BlogController(IBlogService blogService, ILogger<BlogController> logger, IUnitOfWork work)
    {
        _blogService = blogService;
        _logger = logger;
        _repo = work.Get<Blog>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]FindBlog request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        return Ok((await _blogService.GetAsync<BlogView>(new GetRequest<Blog>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Blog>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost("query")]
    public async Task<IActionResult> GetAdmin([FromBody]FindBlog request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        return Ok((await _blogService.GetAsync<BlogView>(new GetRequest<Blog>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Blog>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }
    
    [HttpPost]
    public async Task<ActionResult<BlogView>> Create([FromBody]CreateBlog request)
    {
        
        return Ok(await _blogService.CreateBlogAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find<BlogView>(blog => blog.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Blog)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateBlog request, int id)
    {
        return Ok((await _blogService.UpdateBlogAsync( request, id)).Adapt<BlogView>());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok((await _blogService.DeleteAsync(id)).Adapt<BlogView>());
    }
}