using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Business.Dtos.ServiceDto;
using PhuQuocVoucher.Business.Dtos.ServiceTypeDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class KpiService : IKpiService
{
    private readonly IUnitOfWork _work;

    public KpiService(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<Dictionary<int, ServiceKPI>> GetServiceKpi(IList<int> serviceIds, DateTime? startDate, DateTime? endDate)
    {
        var qrCodes = await _work.Get<QrCode>()
            .Find(qr =>
                serviceIds.Contains(qr.ServiceId) &&
                (qr.QrCodeStatus == QrCodeStatus.Commit || qr.QrCodeStatus == QrCodeStatus.Used)
                && qr.SoldDate >= startDate && qr.SoldDate <= endDate).ToListAsync();
        var qrCodesByServiceId = qrCodes.GroupBy(s => s.ServiceId);
        var serviceKpis = qrCodesByServiceId.Select(g => new ServiceKPI()
        {
            ServiceId = g.Key,
            Revenue = g.Select(qr => qr.ProviderRevenue).Sum(),
            CommissionFee = g.Select(qr => qr.CommissionFee).Sum(),
            SellerCommissionFee = g.Select(qr => qr.SellerCommissionFee).Sum(),
            QrCodeSold = g.Count()
        }).ToDictionary(s => s.ServiceId, s => s);
        return serviceKpis;
    }
    
    public async Task<Dictionary<int, ServiceTypeKPI>> GetServiceTypeKpi(IList<int> serviceTypeIds, DateTime? startDate, DateTime? endDate)
    {
        var qrCodes = await _work.Get<QrCode>()
            .Find(qr =>
                serviceTypeIds.Contains(qr.ServiceTypeId ?? -1)
                && (qr.QrCodeStatus == QrCodeStatus.Commit || qr.QrCodeStatus == QrCodeStatus.Used)
                && qr.SoldDate >= startDate && qr.SoldDate <= endDate)
            .Select(s => new{QrCode = s, s.Service.ServiceTypeId})
            .ToListAsync();
        
        var qrCodesByServiceId = qrCodes.GroupBy(s => s.ServiceTypeId);
        var serviceKpis = qrCodesByServiceId.Select(g => new ServiceTypeKPI()
        {
            CommissionFee = g.Select(qr => qr.QrCode.CommissionFee).Sum(),
            QrCodeSold = g.Count(),
            ServiceTypeId = g.Key
        }).ToDictionary(s => s.ServiceTypeId, s => s);
        return serviceKpis;
    }
    public async Task<Dictionary<int, VoucherKPI>> GetVoucherKpi(IList<int> voucherIds, DateTime? startDate, DateTime? endDate)
    {
        var qrCodes = await _work.Get<OrderItem>()
            .Find(item =>
                voucherIds.Contains(item.VoucherId) 
                && (item.Order.OrderStatus == OrderStatus.Completed || item.Order.OrderStatus == OrderStatus.Used)
                && item.CreateAt >= startDate && item.CreateAt <= endDate).ToListAsync();
        var qrCodesByServiceId = qrCodes.GroupBy(s => s.VoucherId);
        var voucherKpis = qrCodesByServiceId.Select(g => new VoucherKPI()
        {
            VoucherId = g.Key,
            Revenue = g.Select(item => item.ProviderRevenue).Sum(),
            CommissionFee = g.Select(item => item.CommissionFee).Sum(),
            SellerCommissionFee = g.Select(item => item.SellerCommission).Sum(),
            QrCodeSold = g.Count()
        }).ToDictionary(s => s.VoucherId, s => s);
        return voucherKpis;
    }
    
    
    
    public async Task<Dictionary<int, SellerKPI>> GetSellerKpi(IList<int> sellerIds, DateTime? startDate, DateTime? endDate)
    {
        var qrCodes = await _work.Get<OrderItem>()
            .Find(item =>
                sellerIds.Contains(item.SellerId ?? 0) 
                && (item.Order.OrderStatus == OrderStatus.Completed || item.Order.OrderStatus == OrderStatus.Used)
                && item.CreateAt >= startDate && item.CreateAt <= endDate).ToListAsync();
        var qrCodesByServiceId = qrCodes.GroupBy(s => s.SellerId);
        var voucherKpis = qrCodesByServiceId.Select(g => new SellerKPI()
        {
            CommissionFee = g.Select(item => item.CommissionFee).Sum(),
            SellerId = g.Key ?? 0,
            CloseOrder = g.Select(item => item.OrderId).Distinct().Count()
        }).ToDictionary(s => s.SellerId, s => s);
        return voucherKpis;
    }
    
    
}