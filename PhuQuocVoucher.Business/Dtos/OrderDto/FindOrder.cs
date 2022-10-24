using System.Linq.Expressions;
using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderDto;

public class FindOrder : IFindRequest<Order>, IDto
{
    
    [Equal(target:nameof(Order.Id))]
    public int? Id { get; set; }

    [Equal(target:"TotalPrice")]
    public double? TotalPrice { get; set; }

    public OrderStatus? OrderStatus { get; set; }

    [Equal("Customer.Id")]
    public int? CustomerId { get; set; }

    [Equal("Seller.Id")]
    public int? SellerId { get; set; }

    [LessThan("CompleteDate")]
    public DateTime? CompleteDateLowBound { get; set; }

    [BiggerThan("CompleteDate")]
    public DateTime? CompleteDateUpBound { get; set; }

    public Expression<Func<Order, bool>> ToPredicate()
    {
        return order => order.Id == this.Id && order.TotalPrice == this.TotalPrice;
    }
}