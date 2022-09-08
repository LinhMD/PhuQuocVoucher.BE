using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace CrudApiTemplate.Attributes.Search;

public class BiggerThanAttribute : FilterAttribute
{
    public override Expression ToExpressionEvaluate(Expression parameter, object value)
    {
        return Expression.GreaterThanOrEqual(parameter, Expression.Constant(value));
    }
    public BiggerThanAttribute()
    {
    }

    public BiggerThanAttribute(string target,[CallerMemberName] string? name = null) : base(target, name)
    {
    }
}