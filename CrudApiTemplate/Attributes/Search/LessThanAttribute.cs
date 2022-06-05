using System.Linq.Expressions;

namespace CrudApiTemplate.Attributes.Search;

public class LessThanAttribute : FilterAttribute
{
    public override Expression ToExpressionEvaluate(Expression parameter, object value)
    {
        return Expression.LessThanOrEqual(parameter, Expression.Constant(value));
    }
    public LessThanAttribute()
    {
    }

    public LessThanAttribute(string target) : base(target)
    {
    }
}