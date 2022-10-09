using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.PlaceDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]s")]
public class PlaceController : ControllerBase
{
    private readonly IPlaceService _placeService;

    private IUnitOfWork _work;

    private readonly ILogger<CustomerController> _logger;

    private readonly IRepository<Place> _repo;

    public PlaceController(IPlaceService placeService, ILogger<CustomerController> logger, IUnitOfWork work)
    {
        _placeService = placeService;
        _logger = logger;
        _work = work;
        _repo = work.Get<Place>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]FindPlace request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        return Ok((await _placeService.GetAsync(new GetRequest<Place>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Place>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreatePlace request)
    {
        return Ok(await _placeService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find(cus => cus.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Place)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdatePlace request, int id)
    {
        return Ok(await _placeService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _placeService.DeleteAsync(id));
    }

}