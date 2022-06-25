using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.Dtos.ServiceDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/service")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _providerService;
    private readonly IRepository<Service> _repository;
    public ServiceController(IServiceService provider, IUnitOfWork work)
    {
        _providerService = provider;
        _repository = work.Get<Service>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]FindService request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        return Ok((await _providerService.GetAsync<ServiceView>(new GetRequest<Service>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<Service>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateService request)
    {
        return Ok(await _providerService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repository.Find<ServiceView>(ser => ser.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Service)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateService request, int id)
    {
        return Ok(await _providerService.UpdateAsync(id, request));
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _providerService.DeleteAsync(id));
    }
}