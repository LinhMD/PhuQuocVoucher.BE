using System.Linq.Expressions;
using CrudApiTemplate.Utilities;

namespace CrudApiTemplate.Request;

public interface IOrderRequest<TModel>  where TModel: class
{
    IList<OrderModel<TModel>> OrderModels { get; }
}