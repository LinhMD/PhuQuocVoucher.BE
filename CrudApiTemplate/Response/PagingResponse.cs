namespace CrudApiTemplate.Response;

public class PagingResponse<T>
{
    public IList<T> Payload { get; set; }

    public int Total { get; set; }

    public int Page { get; set; }

    public int Size { get; set; }
}