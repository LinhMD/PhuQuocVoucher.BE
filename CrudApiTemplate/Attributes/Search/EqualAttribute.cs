using System.Linq.Expressions;

namespace CrudApiTemplate.Attributes.Search;

public class EqualAttribute : FilterAttribute
{
    public override Expression ToExpressionEvaluate(Expression parameter, object value)
    {
        return Expression.Equal(parameter, Expression.Constant(value));
    }

    public EqualAttribute()
    {
    }

    public EqualAttribute(string target) : base(target)
    {
    }
}