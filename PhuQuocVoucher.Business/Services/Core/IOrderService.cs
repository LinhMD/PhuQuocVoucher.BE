using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IOrderService : IServiceCrud<Order>{

    public Task<(IList<OrderView>, int)> GetOrdersByCustomerId(int cusId, PagingRequest request, OrderRequest<Order> sortBy);
    
}