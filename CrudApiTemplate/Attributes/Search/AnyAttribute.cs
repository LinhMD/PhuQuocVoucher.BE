﻿using System.Linq.Expressions;
using System.Reflection;

namespace CrudApiTemplate.Attributes.Search;

///Ex: User.Profiles.Any(Profile => Profile.Gender == ProfileGender)
public class AnyAttribute : FilterAttribute
{
    private static readonly MethodInfo AnyMethod = typeof(Enumerable).GetMethods().Single(m => m.Name == "Any" && m.GetParameters().Length == 2);
    private FilterAttribute Filter { get; }


    private readonly string _property;
    public AnyAttribute(string target, string property, Type filterType) : base(target)
    {

        if (!filterType.IsSubclassOf(typeof(FilterAttribute)))
            throw new Exception("Coding error of using AnyAttribute");

        Filter = (FilterAttribute?) Activator.CreateInstance(filterType, property) ?? new EqualAttribute(property);

        _property = property;
    }
    public override Expression ToExpressionEvaluate(Expression parameter, object value)
    {
        var parameterType = parameter.Type.GetTypeInfo().GenericTypeArguments[0];
        //Profile
        var innerParameter = Expression.Parameter(parameterType, parameterType.Name);

        var members = _property.Split(".");
        var memberExpression = Expression.Property(innerParameter, members[0]);
        foreach (var member in members.Skip(1))
        {
            memberExpression = Expression.Property(innerParameter, member);
        }
        //Profile.Gender == true;
        var innerBody = Filter.ToExpressionEvaluate(memberExpression, value);
        //Profile => Profile.Gender == true
        var innerLambda = Expression.Lambda(innerBody, innerParameter);
        var anyMethod = AnyMethod.MakeGenericMethod(parameterType);
        //User.Profiles.Any(Profile => (Profile.Gender == True))
        return Expression.Call(null, anyMethod, parameter, innerLambda);
    }
}