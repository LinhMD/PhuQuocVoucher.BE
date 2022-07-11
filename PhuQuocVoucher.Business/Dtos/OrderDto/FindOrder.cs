using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderDto;

public class FindOrder : IFindRequest<Order>, IDto
{
    public int? Id { get; set; }

    public double? TotalPrice { get; set; }

    public OrderStatus? OrderStatus { get; set; }

    [Equal("Customer.Id")]
    public int? CustomerId { get; set; }

    [Equal("Seller.Id")]
    public int? SellerId { get; set; }


}