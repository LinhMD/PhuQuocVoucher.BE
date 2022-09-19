using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.CartDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IOrderService : IServiceCrud<Order>{

    public Task<(IList<OrderView>, int)> GetOrdersByCustomerId(int cusId, PagingRequest request, OrderRequest<Order> sortBy);

    public Task<OrderView> CreateOrderAsync(CreateOrder createOrder);

    public Task<OrderView> PlaceOrderAsync(CartView cart, int cusId, int? sellerId = null);
}