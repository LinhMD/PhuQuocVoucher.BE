using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.ProviderDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;
using ServiceProvider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Authorize(Roles = nameof(Role.Admin))]
[Route("api/v1/[controller]s")]
public class ProviderController : ControllerBase
{
    private readonly IProviderService _providerService;
    private readonly IRepository<ServiceProvider> _repository;
    private IUnitOfWork _work;
    public ProviderController(IProviderService provider, IUnitOfWork work)
    {
        _providerService = provider;
        _work = work;
        _repository = work.Get<ServiceProvider>();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery]FindProvider request, [FromQuery]PagingRequest paging, string? orderBy)
    {
        return Ok((await _providerService.GetAsync<ProviderView>(new GetRequest<ServiceProvider>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<ServiceProvider>(),
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
        return Ok(await _repository.Find<ProviderView>(cus => cus.Id == id).FirstOrDefaultAsync() ??
                  throw new ModelNotFoundException($"Not Found {nameof(ServiceProvider)} with id {id}"));
    }

    
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Provider)}")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateProvider request, int id, [FromClaim("ProviderId")] int? providerId)
    {
        return Ok(await _providerService.UpdateAsync(providerId ?? id, request));
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _providerService.DeleteAsync(id));
    }

    [HttpPost("scan-qr")]
    public async Task<IActionResult> ScanQrCode(string hashCode, [FromClaim("ProviderId")] int? providerId)
    {
        var qrCodeId = await _work.Get<OrderItem>()
            .Find(item => item.QrCode != null && item.QrCodeId != null && item.QrCode.HashCode == hashCode &&
                          item.ProviderId == providerId).Select(item => item.QrCodeId).FirstOrDefaultAsync();
        if (qrCodeId == null)
        {
            throw new ModelNotFoundException("Qr code Not found");
        }

        var qrCode = await _work.Get<QrCodeInfo>().Find(info => info.Id == qrCodeId).FirstOrDefaultAsync();
        
        if (qrCode == null){
            throw new ModelNotFoundException("Qr code Not found");
        }

        qrCode.Status = QRCodeStatus.Used;
        await _work.CompleteAsync();
        return Ok();
    }
    
    
    
}