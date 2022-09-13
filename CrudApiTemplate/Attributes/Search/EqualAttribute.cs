using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace CrudApiTemplate.Attributes.Search;

public class EqualAttribute : FilterAttribute
{
    public override Expression ToExpressionEvaluate(Expression parameter, object value)
    {
        return Expression.Equal(parameter, Expression.Convert(Expression.Constant(value), parameter.Type));
    }

    public EqualAttribute()
    {
    }

    public EqualAttribute(string target, [CallerMemberName] string? name = null) : base(target, name)
    {
    }
}