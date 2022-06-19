using CrudApiTemplate.Request;
using CrudApiTemplate.Response;

namespace CrudApiTemplate.Utilities;

public static class ExtensionMethods
{
    public static MyResponse<T> ToMyResponse<T>(this T t, int code, string message)
    {
        return new MyResponse<T>()
        {
            Data = t,
            Code = code,
            Message = message
        };
    }

    public static PagingResponse<T> ToPagingResponse<T>(this (IList<T> models, int total) tuple, PagingRequest request)
    {
        var (models, total) = tuple;
        return new PagingResponse<T>
        {
            Page = request.Page,
            Payload = models,
            Size = request.PageSize,
            Total = total
        };
    }
}