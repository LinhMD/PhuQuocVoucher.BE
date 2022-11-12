using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using PhuQuocVoucher.Api.Ultility;

namespace CrudApiTemplate.Attributes.Search;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = true)]
public abstract class FilterAttribute : Attribute
{
    protected FilterAttribute(string target, string? propertyName)
    {
        Target = target;
        PropertyName = propertyName;

    }

    protected FilterAttribute()
    {
    }

    public string? Target { get;  }

    public string? PropertyName { get; }



    public abstract Expression ToExpressionEvaluate(Expression parameter, object value);
}