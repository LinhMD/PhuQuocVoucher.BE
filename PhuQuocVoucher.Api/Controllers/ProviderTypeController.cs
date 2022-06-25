using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using PhuQuocVoucher.Api.Dtos.ProviderTypeDto;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/providerType")]

[CrudExceptionFilter]
public class ProviderTypeController : ControllerBase
{
    private readonly IProviderTypeService _providerTypeService;

    public ProviderTypeController(IProviderTypeService providerType)
    {
        _providerTypeService = providerType;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]FindProviderType request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        return Ok((await _providerTypeService.GetAsync(new GetRequest<ProviderType>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<ProviderType>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProviderType request)
    {
        return Ok(await _providerTypeService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _providerTypeService.GetAsync(id));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateProviderType request, int id)
    {
        return Ok(await _providerTypeService.UpdateAsync(id, request));
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _providerTypeService.DeleteAsync(id));
    }
}