using System.Linq.Expressions;
using CrudApiTemplate.Request;

namespace CrudApiTemplate.Utilities;

public class GetRequest<TModel>  where TModel: class
{
    public OrderRequest<TModel> OrderRequest { get; set; }
    public IFindRequest<TModel> FindRequest { get; set; }
    public PagingRequest PagingRequest { get; set; } = new();

    public PagingRequest GetPaging()
    {
        return PagingRequest;
    }
}