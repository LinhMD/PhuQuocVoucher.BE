using System.Linq.Expressions;

namespace CrudApiTemplate.Attributes.Search;

public class ContainAttribute : FilterAttribute
{
    public override Expression ToExpressionEvaluate(Expression parameter, object value)
    {
        var parameterType = parameter.Type;
        var containMethod = parameterType.GetMethod("Contains", new []{parameterType});
        if (containMethod is null) throw new Exception("Coding error: using ContainAttribute");
        return Expression.Call(parameter, containMethod, Expression.Constant(value));
    }

    public ContainAttribute()
    {
    }

    public ContainAttribute(string target) : base(target)
    {
    }
}