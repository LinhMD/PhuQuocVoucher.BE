namespace CrudApiTemplate.Request;

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