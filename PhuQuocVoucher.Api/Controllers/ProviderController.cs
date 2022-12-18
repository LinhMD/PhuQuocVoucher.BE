using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.ProviderDto;
using PhuQuocVoucher.Business.Dtos.QrCodeDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;
using ServiceProvider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]s")]
[Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Provider)}")]
public class ProviderController : ControllerBase
{
    private readonly IProviderService _providerService;
    private readonly IRepository<ServiceProvider> _repository;
    private readonly IUnitOfWork _work;
    private readonly IKpiService _kpiService;

    public ProviderController(IProviderService provider, IUnitOfWork work, IKpiService kpiService)
    {
        _providerService = provider;
        _work = work;
        _kpiService = kpiService;
        _repository = work.Get<ServiceProvider>();
    }
    
    [HttpGet("Admin")]
    public async Task<IActionResult> GetAdmin([FromQuery]FindProvider request, [FromQuery]PagingRequest paging, string? orderBy,
        DateTime? kpiStartDate, DateTime? kpiEndDate)
    {
        var (models, total) = await _providerService.GetAsync<ProviderView>(new GetRequest<ServiceProvider>
        {
            FindRequest = request,
            OrderRequest = orderBy.ToOrderRequest<ServiceProvider>(),
            PagingRequest = paging
        });
        var providerIds = models.Select(s => s.Id).ToList();

        var providerKpis = await _kpiService.GetProviderKpi(providerIds, kpiStartDate, kpiEndDate);
        
        foreach (var provider in models)
        {
            providerKpis.TryGetValue(provider.Id , out var kpi);
            provider.Kpi = kpi ?? new ProviderKpi();
        }
        
        return Ok((models, total).ToPagingResponse(paging));
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

    //todo:
    [Authorize(Roles = $"{nameof(Role.Provider)}")]
    [HttpPost("scan-qr")]
    public async Task<IActionResult> ScanQrCode(string hashCode, [FromClaim("ProviderId")] int? providerId)
    {
        var qrCodeId = await _work.Get<QrCode>()
            .Find(item =>  item.HashCode == hashCode &&
                          item.ProviderId == providerId).Select(item => item.Id).FirstOrDefaultAsync();
        if (qrCodeId == 0)
        {
            return BadRequest(new
            {
                Data = default(QrCodeView),
                Message = "Không tìm thấy Voucher"
            });
        }

        var qrCode = await _work.Get<QrCode>().Find(info => info.Id == qrCodeId).FirstOrDefaultAsync();
        
        if (qrCode == null){
            return BadRequest(new
            {
                Data = default(QrCodeView),
                Message = "Không tìm thấy Voucher"
            });
        }

        if (qrCode.QrCodeStatus != QrCodeStatus.Commit)  
            return BadRequest(new
            {
                Data = default(QrCodeView),
                Message = "Voucher chưa đủ điều kiện sửa dụng"
            });
        
        qrCode.QrCodeStatus = QrCodeStatus.Used;
        qrCode.UseDate = DateTime.Now;
        
        await _work.CompleteAsync();
        
        return Ok(new
        {
            Data = await _work.Get<QrCode>()
                .Find<QrCodeView>(item =>  item.HashCode == hashCode &&
                                          item.ProviderId == providerId).FirstOrDefaultAsync(),
            Message = "Voucher đã sử dụng"
        });

    }
    
    [Authorize(Roles = $"{nameof(Role.Provider)}")]
    [HttpPost("qr-item")]
    public async Task<IActionResult> GetOrderItem(string hashCode, [FromClaim("ProviderId")] int? providerId)
    {
        var item = await _work.Get<QrCode>()
            .Find<QrCodeView>(item =>  item.HashCode == hashCode &&
                          item.ProviderId == providerId).FirstOrDefaultAsync();
        if (item == null)
        {
            return BadRequest(new
            {
                Data = item,
                Message = "Không tìm thấy Qr code"
            });
        }
        
        if (item.QrCodeStatus == QrCodeStatus.Commit)  
            return BadRequest(new
            {
                Data = item,
                Message = "Vé đủ điều kiện sửa dụng"
            });
        if(item.QrCodeStatus == QrCodeStatus.Used)
            return Ok(new
        {
            Data = item,
            Message = "Voucher đã sử dụng"
        });
        return BadRequest(new
        {
            Data = default(QrCodeView),
            Message = "Voucher chưa đủ điều kiện sửa dụng"
        });
    }
    
    [Authorize(Roles = $"{nameof(Role.Provider)}")]
    [HttpPost("order-item")]
    public async Task<IActionResult> GetOrderItem(int orderItemId, [FromClaim("ProviderId")] int? providerId)
    {
        var item = await _work.Get<QrCode>()
            .Find<QrCodeView>(item => item.Id == orderItemId && item.ProviderId == providerId).FirstOrDefaultAsync();
        if (item == null)
        {
            return BadRequest(new
            {
                Data = item,
                Message = "Không tìm thấy Qr code"
            });
        }
        
        if (item.QrCodeStatus == QrCodeStatus.Commit)  
            return BadRequest(new
            {
                Data = item,
                Message = "Vé đủ điều kiện sửa dụng"
            });
        if(item.QrCodeStatus == QrCodeStatus.Used)
            return Ok(new
            {
                Data = item,
                Message = "Voucher đã sử dụng"
            });
        return BadRequest(new
        {
            Data = default(QrCodeView),
            Message = "Voucher chưa đủ điều kiện sửa dụng"
        });
    }
    
}