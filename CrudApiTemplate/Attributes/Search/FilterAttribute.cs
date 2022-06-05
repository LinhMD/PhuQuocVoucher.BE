using System.Linq.Expressions;

namespace CrudApiTemplate.Attributes.Search;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = true)]
public abstract class FilterAttribute : Attribute
{
    protected FilterAttribute(string target)
    {
        Target = target;
    }

    protected FilterAttribute()
    {
    }

    public string? Target { get;  }
    public abstract Expression ToExpressionEvaluate(Expression parameter, object value);
}