using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface ISellerService : IServiceCrud<Seller>
{
    public Task<(IList<SellerView>, int)> FindSellerAsync(GetRequest<Seller> request, DateTime? completeDateLowBound);

    public Task<SellerKpiView> GetSellerKpis(int sellerId, int month, int year);
}