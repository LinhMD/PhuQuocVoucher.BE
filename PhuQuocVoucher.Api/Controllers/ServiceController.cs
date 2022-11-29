using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.ServiceDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Provider)}")]
[Route("api/v1/[controller]s")]
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
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery]FindService request, [FromQuery]PagingRequest paging, string? orderBy, [FromClaim("ProviderId")] int? providerId)
    {
        request.ProviderId = providerId;
        return Ok((await _providerService.GetAsync<ServiceView>(new GetRequest<Service>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<Service>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateService request, [FromClaim("ProviderId")] int? providerId)
    {
        request.ProviderId = providerId ?? request.ProviderId;
        return Ok(await _providerService.CreateAsync(request));
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, [FromClaim("ProviderId")] int? providerId)
    {
        return Ok(await _repository.Find<ServiceView>(ser => ser.Id == id && (providerId == null || ser.ProviderId == providerId)).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(Service)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateService request, int id, [FromClaim("ProviderId")] int? providerId)
    {
        var serviceId = await _repository.Find(ser => providerId == null || ser.ProviderId == providerId).Select(ser => ser.Id).ToListAsync();
        
        if (serviceId.Contains(id))
            return Ok(await _providerService.UpdateAsync(id, request));
        
        return Challenge();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, [FromClaim("ProviderId")] int? providerId)
    {
        var serviceId = await _repository.Find(ser => providerId == null || ser.ProviderId == providerId).Select(ser => ser.Id).ToListAsync();
        if (serviceId.Contains(id))
            return Ok(await _providerService.DeleteAsync(id));
        return Challenge();
    }
}