using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class KpiController : ControllerBase
{
    private readonly IUnitOfWork _work;


    public KpiController(IUnitOfWork work)
    {
        _work = work;
    }


    [HttpGet("range")]
    public async Task<dynamic> GetRevenue(DateTime? kpiStartDate, DateTime? kpiEndDate)
    {
        var items = await _work.Get<OrderItem>()
            .Find(item => 
                (item.Order.OrderStatus == OrderStatus.Completed || item.Order.OrderStatus == OrderStatus.Used) 
                && item.CreateAt >= kpiStartDate 
                && item.CreateAt <= kpiEndDate
                )
            .Select(item => new 
            {
                item.CommissionFee,
                item.SellerCommission, 
                item.ProviderRevenue,
                item.OrderId, 
                item.VoucherId, 
                item.Quantity,
                QrCode = item.QrCodes.Count(),
                item.IsCombo,
            }).ToListAsync();
        
        var commissionFee =  items.Select(i => i.CommissionFee).Sum();
        var sellerCommission =  items.Select(i => i.SellerCommission).Sum();
        var providerRevenue =  items.Select(i => i.ProviderRevenue).Sum();
        var orderSold =  items.Select(i => i.OrderId).Distinct().Count();
        var qrCodeSold = items.Select(i => i.QrCode).Sum();
        return  new{commissionFee, sellerCommission, providerRevenue, orderSold, qrCodeSold };
    }
    
    [HttpGet("year")]
    public async Task<object> GetRevenuePerMonth([Range(2000, 2100)]int year)
    {
        var items = await _work.Get<OrderItem>()
            .Find(item => 
                item.CreateAt != null
                && (item.Order.OrderStatus == OrderStatus.Completed || item.Order.OrderStatus == OrderStatus.Used) 
                && item.CreateAt.Value.Year == year)
            .Select(item => new
            {
                item.CommissionFee, 
                item.SellerCommission, 
                item.ProviderRevenue,
                item.OrderId,
                item.VoucherId, 
                item.Quantity,
                QrCode = item.QrCodes.Count,
                item.CreateAt!.Value.Month,
                item.QrCodes.Count,
                item.IsCombo
            }).ToListAsync();
        
        var groupBy = items.GroupBy(item => item.Month);
        
        var results = groupBy.Select(g => new
        {
            CommissionFee = g.Select(i => i.CommissionFee).Sum(),
            SellerCommission = g.Select(i => i.SellerCommission).Sum(),
            ProviderRevenue = g.Select(i => i.ProviderRevenue).Sum(),
            OrderSold = g.Select(i => i.OrderId).Distinct().Count(),
            Month = g.Key,
            QrCodeSold = g.Select(i => i.QrCode).Sum(),
            VoucherSold = g.Where(i => !i.IsCombo).Select(i => i.Quantity).Sum(),
            ComboSold =  g.Where(i => i.IsCombo).Select(i => i.Quantity).Sum()
        }).ToDictionary(g => g.Month, g => g);
        return results;
    }

    
    [HttpGet("range-provider")]
    public async Task<dynamic> GetRevenueProvider(DateTime? kpiStartDate, DateTime? kpiEndDate, [FromClaim("ProviderId")] int providerId)
    {
        var qrCode = await _work.Get<QrCode>()
            .Find(qr => 
                (qr.QrCodeStatus == QrCodeStatus.Commit || qr.QrCodeStatus == QrCodeStatus.Used) 
                && qr.SoldDate >= kpiStartDate 
                && qr.SoldDate <= kpiEndDate
                && qr.ProviderId == providerId
            )
            .Select(qr => new 
            {
                qr.CommissionFee,
                qr.SellerCommissionFee, 
                qr.ProviderRevenue,
                qr.OrderId, 
                qr.VoucherId, 
            }).ToListAsync();
        
        var commissionFee =  qrCode.Select(i => i.CommissionFee).Sum();
        var sellerCommission =  qrCode.Select(i => i.SellerCommissionFee).Sum();
        var providerRevenue =  qrCode.Select(i => i.ProviderRevenue).Sum();
        var orderSold =  qrCode.Select(i => i.OrderId).Distinct().Count();
        var qrCodeSold = qrCode.Count;
        return  new{ commissionFee, sellerCommission, providerRevenue, orderSold, qrCodeSold };
    }
    
    [HttpGet("year-provider")]
    public async Task<object> GetRevenuePerMonthProvider([Range(2000, 2100)]int year, [FromClaim("ProviderId")] int providerId)
    {
        var items = await _work.Get<QrCode>()
            .Find(qr => 
                qr.SoldDate != null
                && (qr.QrCodeStatus == QrCodeStatus.Commit || qr.QrCodeStatus == QrCodeStatus.Used)
                && qr.SoldDate.Value.Year == year
                && qr.ProviderId == providerId)
            .Select(item => new
            {
                item.CommissionFee, 
                item.SellerCommissionFee, 
                item.ProviderRevenue,
                item.OrderId,
                item.VoucherId, 
                item.CreateAt!.Value.Month,
            }).ToListAsync();
        
        var groupBy = items.GroupBy(item => item.Month);
        
        var results = groupBy.Select(g => new
        {
            CommissionFee = g.Select(i => i.CommissionFee).Sum(),
            SellerCommission = g.Select(i => i.SellerCommissionFee).Sum(),
            ProviderRevenue = g.Select(i => i.ProviderRevenue).Sum(),
            OrderSold = g.Select(i => i.OrderId).Distinct().Count(),
            Month = g.Key,
            QrCodeSold = g.Count(),
        }).ToDictionary(g => g.Month, g => g);
        return results;
    }
    
}