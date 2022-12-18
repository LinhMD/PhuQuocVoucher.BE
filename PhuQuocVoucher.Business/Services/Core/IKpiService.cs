using PhuQuocVoucher.Business.Dtos.ProviderDto;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Business.Dtos.ServiceDto;
using PhuQuocVoucher.Business.Dtos.ServiceTypeDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IKpiService
{
    public Task<Dictionary<int, ServiceKPI>> GetServiceKpi(IList<int> serviceIds, DateTime? startDate, DateTime? endDate);


    public Task<Dictionary<int, VoucherKPI>> GetVoucherKpi(IList<int> voucherIds, DateTime? startDate, DateTime? endDate);


    public Task<Dictionary<int, SellerKPI>> GetSellerKpi(IList<int> sellerIds, DateTime? startDate, DateTime? endDate);

    public Task<Dictionary<int, ServiceTypeKPI>> GetServiceTypeKpi(IList<int> serviceTypeIds, DateTime? startDate,
        DateTime? endDate);


    public Task<Dictionary<int, VoucherKPI>> GetVoucherKpiOfSeller(int sellerId, DateTime? startDate, DateTime? endDate);

    public  Task<Dictionary<int, ProviderKpi>> GetProviderKpi(IList<int> providerIds, DateTime? startDate,
        DateTime? endDate);

}