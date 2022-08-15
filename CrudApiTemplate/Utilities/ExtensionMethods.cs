using System.Text.RegularExpressions;
using CrudApiTemplate.CustomException;
using CrudApiTemplate.Request;
using CrudApiTemplate.Response;
using PhuQuocVoucher.Api.Ultility;

namespace CrudApiTemplate.Utilities;

public static class ExtensionMethods
{


    public static readonly Regex SortByRegex = new Regex(Common.SortByRegexString);

    public static OrderRequest<TModel> ToOrderRequest<TModel>(this string? orderBy) where TModel : class
    {
        if (orderBy == null)
            return new OrderRequest<TModel>();

        if (!SortByRegex.IsMatch(orderBy))
            throw new ModelValueInvalidException("OrderBy Parameter invalid");

        return new OrderRequest<TModel>()
        {
            OrderModels = orderBy.Split(",").Select(s => new OrderModel<TModel>()
            {
                IsAscending = s.Contains("-asc"),
                PropertyName = s.Split("-").First().FirstCharToUpper()
            }).ToList()
        };
    }

    public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => "",
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };

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