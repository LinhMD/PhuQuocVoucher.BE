using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Dtos.ProviderDto;
using ServiceProvider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/provider")]
[CrudExceptionFilter]
public class ProviderController : ControllerBase
{
    private readonly IProviderService _providerService;
    private readonly IRepository<ServiceProvider> _repository;
    public ProviderController(IProviderService provider, IUnitOfWork work)
    {
        _providerService = provider;
        _repository = work.Get<ServiceProvider>();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]FindProvider request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        return Ok((await _providerService.GetAsync(new GetRequest<ServiceProvider>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<ServiceProvider>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProvider request)
    {
        return Ok(await _providerService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repository.Find<ProviderSView>(cus => cus.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(ServiceProvider)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateProvider request, int id)
    {
        return Ok(await _providerService.UpdateAsync(id, request));
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _providerService.DeleteAsync(id));
    }
}