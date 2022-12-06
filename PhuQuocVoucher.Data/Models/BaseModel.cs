using System.Linq.Expressions;
using System.Text.Json.Serialization;
using CrudApiTemplate.OrderBy;
using PhuQuocVoucher.Data.Repositories;

namespace PhuQuocVoucher.Data.Models;

public class BaseModel
{
    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeleteAt { get; set; }
    public ModelStatus Status { get; set; }


    public static void SetUpOrderBy<TModel>() where TModel : BaseModel
    {
        Expression<Func<TModel, DateTime?>> createAt = voucher => voucher.CreateAt;
        Expression<Func<TModel, DateTime?>> updateAt = voucher => voucher.UpdateAt;
        Expression<Func<TModel, DateTime?>> deleteAt = voucher => voucher.DeleteAt;
        Expression<Func<TModel, ModelStatus>> orderByStatus = voucher => voucher.Status;
        var orderFields = new List<(string, dynamic)>()
        {
            (nameof(CreateAt), createAt),
            (nameof(UpdateAt), updateAt), 
            (nameof(DeleteAt), deleteAt),
            (nameof(Status), orderByStatus)
        };
        foreach (var (name, expression) in orderFields)
        {
            OrderByProvider<TModel>.OrderByDic.Add(name, expression);
        }
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ModelStatus
{
    Active,
    Disable,
    New
}