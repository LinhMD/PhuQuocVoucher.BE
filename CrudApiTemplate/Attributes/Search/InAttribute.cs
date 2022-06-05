using System.Linq.Expressions;

namespace CrudApiTemplate.Attributes.Search;

public class InAttribute : FilterAttribute
{
    public override Expression ToExpressionEvaluate(Expression parameter, object value)
    {
        var parameterType = parameter.Type;
        var containMethod = value.GetType().GetMethod("Contains", new[]{parameterType});

        if (containMethod is null) throw new Exception("Coding error at InAttribute");
        return Expression.Call(Expression.Constant(value), containMethod, parameter);
    }

    public InAttribute()
    {
    }

    public InAttribute(string target) : base(target)
    {
    }
}