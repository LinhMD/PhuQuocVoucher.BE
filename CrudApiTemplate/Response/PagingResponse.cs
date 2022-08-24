namespace CrudApiTemplate.Response;

public class PagingResponse<T>
{
    public IList<T> Data { get; set; }

    public int Total { get; set; }

    public int Current { get; set; }

    public int PageSize { get; set; }
}