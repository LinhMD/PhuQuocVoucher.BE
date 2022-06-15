using CrudApiTemplate.Utilities;

namespace CrudApiTemplate.Request;

public class OrderRequest<TModel> : IOrderRequest<TModel> where TModel : class
{
    public IList<OrderModel<TModel>> OrderModels { get; set; } = new List<OrderModel<TModel>>();
}