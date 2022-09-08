using System.Linq.Expressions;
using System.Reflection;
using CrudApiTemplate.Attributes.Search;
using PhuQuocVoucher.Api.Ultility;

namespace CrudApiTemplate.Request;

public interface IFindRequest<TModel> where TModel: class
{
    virtual Expression<Func<TModel, bool>> ToPredicate()
    {
        var param = Expression.Parameter(typeof(TModel), typeof(TModel).Name);
        Expression expressionBody = Expression.Constant(true);
        foreach (var property in GetType().GetProperties())
        {
            var value = property?.GetValue(this);
            if(value is null) continue;

            Expression tProperty;
            var filters = Attribute.GetCustomAttributes(property!, typeof(FilterAttribute)) as FilterAttribute[] ?? Array.Empty<FilterAttribute>();
            if (filters.Any())
            {
                foreach (var filter in filters)
                {
                    Console.WriteLine(filter.Target);
                    var list = filter.Target?.Split(".").ToList();
                    //ex: t.Name
                    tProperty = Navigate(param, list, property);
                    expressionBody = Expression.And(expressionBody, filter.ToExpressionEvaluate(tProperty, value)!);
                }
            }
            else
            {
                tProperty = Expression.Property(param, property!.Name);
                expressionBody = Expression.And(expressionBody, Expression.Equal(tProperty, Expression.Constant(value))!);
            }
        }

        //ex: t => ((t.Name == "nah") && (t.Role.Name == "admin"))
        var lambda = Expression.Lambda<Func<TModel, bool>>(expressionBody, param);
        Console.WriteLine(lambda);
        return lambda;
    }

    private static Expression Navigate(Expression param, IList<string>? list, MemberInfo? property)
    {
        //if have more member navigation like t.Role.Name
        Expression tProperty = Expression.Property(param, list?[0] ?? property!.Name);

        return list == null ? tProperty : list.Skip(1).Aggregate(tProperty, Expression.Property);
    }
}