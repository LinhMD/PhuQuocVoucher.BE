using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Dtos.RankDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class RankController : ControllerBase
{
    private readonly IRankService _rankService;

    private readonly ILogger<RankController> _logger;

    private readonly IRepository<SellerRank> _repo;

    private readonly IUnitOfWork _work;
    
    private readonly IMailingService _mailingService;

    public RankController(IRankService rankService, ILogger<RankController> logger, IUnitOfWork work, IMailingService mailingService)
    {
        _rankService = rankService;
        _logger = logger;
        _work = work;
        _mailingService = mailingService;
        _repo = work.Get<SellerRank>();
        
    }

    [HttpGet]
    public async Task<ActionResult<RankView>> Get([FromQuery] FindRank request, [FromQuery] PagingRequest paging, string? orderBy)
    {
        return Ok((await _rankService.GetAsync<RankView>(new GetRequest<SellerRank>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<SellerRank>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find<RankView>(order => order.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(SellerRank)} with id {id}"));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateRank request)
    {
        return Ok(await _rankService.CreateAsync(request));
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateRank request, int id)
    {
        return Ok((await _rankService.UpdateAsync(id, request)));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok((await _rankService.DeleteAsync(id)));
    }
    
}