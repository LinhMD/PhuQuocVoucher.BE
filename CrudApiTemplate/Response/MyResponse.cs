namespace CrudApiTemplate.Response;

public class MyResponse<T>
{
    public int Code { get; set; }
    public T Data { get; init; }
    public string? Message { get; set; }

}