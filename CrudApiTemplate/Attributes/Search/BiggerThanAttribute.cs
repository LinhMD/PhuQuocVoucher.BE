using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace CrudApiTemplate.Attributes.Search;

public class BiggerThanAttribute : FilterAttribute
{
    public override Expression ToExpressionEvaluate(Expression parameter, object value)
    {
        return Expression.GreaterThanOrEqual(parameter, Expression.Convert(Expression.Constant(value), parameter.Type));
    }
    public BiggerThanAttribute()
    {
    }

    public BiggerThanAttribute(string target,[CallerMemberName] string? name = null) : base(target, name)
    {
    }
}