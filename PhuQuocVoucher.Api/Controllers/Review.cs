using CrudApiTemplate.CustomException;
using CrudApiTemplate.ExceptionFilter;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.ReviewDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/review")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    private readonly ILogger<ReviewController> _logger;

    private readonly IRepository<Review> _repo;

    public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger, IUnitOfWork work)
    {
        _reviewService = reviewService;
        _logger = logger;
        _repo = work.Get<Review>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindReview request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _reviewService.GetAsync(new GetRequest<Review>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<Review>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReview request)
    {
        return Ok(await _reviewService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find(review => review.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Review)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateReview request, int id)
    {
        return Ok(await _reviewService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _reviewService.DeleteAsync(id));
    }
}