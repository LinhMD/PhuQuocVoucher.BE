using System.Linq.Expressions;
using CrudApiTemplate.Utilities;

namespace CrudApiTemplate.Request;

public interface IOrderRequest<TModel> : IFindRequest<TModel> where TModel: class
{
    PagingRequest GetPaging();

    IList<OrderModel<TModel>> OrderModels { get; }
}