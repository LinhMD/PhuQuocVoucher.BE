namespace CrudApiTemplate.Request;

public class PagingRequest
{
    public string? SortBy { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}