using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace CrudApiTemplate.Attributes.Search;

public class InAttribute : FilterAttribute
{
    public override Expression ToExpressionEvaluate(Expression parameter, object value)
    {
        var parameterType = parameter.Type;
        var containMethod = value.GetType().GetMethod("Contains", new[]{parameterType});

        if (containMethod is null) throw new Exception($"Coding error at InAttribute: type {value.GetType()} do not have Contains Method");

        //value.Contains(parameter)
        return Expression.Call(Expression.Constant(value), containMethod, parameter);
    }

    public InAttribute()
    {
    }

    public InAttribute(string target,[CallerMemberName] string? name = null) : base(target, name)
    {
    }
}