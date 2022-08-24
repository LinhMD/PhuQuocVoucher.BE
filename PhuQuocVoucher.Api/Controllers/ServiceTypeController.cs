using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Api.ExceptionFilter;
using PhuQuocVoucher.Business.Dtos.ServiceTypeDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class ServiceTypeController : ControllerBase
{
    private readonly IServiceTypeService _serviceTypeService;

    private readonly ILogger<ServiceTypeController> _logger;

    private readonly IRepository<ServiceType> _repo;

    public ServiceTypeController(IServiceTypeService serviceTypeService,
        ILogger<ServiceTypeController> logger,
        IUnitOfWork work)
    {
        _serviceTypeService = serviceTypeService;
        _logger = logger;
        _repo = work.Get<ServiceType>();
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] FindServiceType request,
        [FromQuery] PagingRequest paging,
        string? orderBy)
    {
        return Ok((await _serviceTypeService.GetAsync(new GetRequest<ServiceType>
        {
            FindRequest = request,
            OrderRequest = new OrderRequest<ServiceType>(),
            PagingRequest = paging
        })).ToPagingResponse(paging));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceType request)
    {
        return Ok(await _serviceTypeService.CreateAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repo.Find(serviceType => serviceType.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(ServiceType)} with id {id}"));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateServiceType request, int id)
    {
        return Ok(await _serviceTypeService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _serviceTypeService.DeleteAsync(id));
    }
}