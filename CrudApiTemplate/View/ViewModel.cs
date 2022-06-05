

using System.Linq.Expressions;
using System.Reflection;
using CrudApiTemplate.Attributes;

namespace CrudApiTemplate.View;

public abstract class ViewModel<TModel> : IView<TModel> where TModel : class
{
    /*public ViewModel<TModel> InitData(TModel? model)
    {
        var modelType = typeof(TModel);
        foreach (var viewProperty in GetType().GetProperties())
        {
            var targetAttr = Attribute.GetCustomAttribute(viewProperty, typeof(TargetAttribute)) as TargetAttribute;
            //Role.Name
            string targetPath = targetAttr?.TargetPath ?? viewProperty.Name;

            //Role
            var modelProperty = modelType.GetProperty(targetPath.Split(".")[0]);

            //check null and get value
            if(modelProperty is null) continue;
            object? value = modelProperty.GetValue(model);
            if(value is null) continue;

            //get inner value if having
            foreach (var path in targetPath.Split(".").Skip(1))
            {
                var innerProperty = value!.GetType().GetProperty(path);
                //Console.WriteLine(innerProperty);
                value = innerProperty?.GetValue(value);
            }

            if(value is null) continue;


            if (viewProperty.PropertyType == value?.GetType())
            {
                viewProperty.SetValue(this, value);
            }
            else if (viewProperty.PropertyType == typeof(IEnumerable<ViewModel<object>>))
            {

            }
            else if (viewProperty.PropertyType.GetInterfaces().Any( i => i.IsGenericType && i.GetGenericTypeDefinition( ) == typeof(IView<>)))
            {
                var instance = Activator.CreateInstance(viewProperty.PropertyType);
                if (instance != null)
                    (instance as IView<object>)?.InitData(value);

                viewProperty.SetValue(this, instance);
                Console.WriteLine(instance);
            }

        }

        return this;
    }*/


    public ViewModel<TModel> InitData(TModel? model)
    {
        Expression<Func<TModel, ViewModel<TModel>>> mappingExpression;
        var para = Expression.Parameter(this.GetType(), this.GetType().Name);




        return null;
    }

    public static Expression<Func<TModel, TView>> NewExpression<TView>() where TView : class, IView<TModel>
    {

        return null;
    }
    public IQueryable<TModel> DynamicInclude(IQueryable<TModel> dbSet)
    {
        return dbSet;
    }

    public abstract void SetupMapping();
    public static Expression<Func<TSource, TTarget>> BuildSelector<TSource, TTarget>(string members) =>
        BuildSelector<TSource, TTarget>(members.Split(',').Select(m => m.Trim()));

    public static Expression<Func<TSource, TTarget>> BuildSelector<TSource, TTarget>(IEnumerable<string> members)
    {
        var parameter = Expression.Parameter(typeof(TSource), "e");
        var body = NewObject(typeof(TTarget), parameter, members.Select(m => m.Split('.')));
        return Expression.Lambda<Func<TSource, TTarget>>(body, parameter);
    }

    static Expression NewObject(Type targetType, Expression source, IEnumerable<string[]> memberPaths, int depth = 0)
    {
        var bindings = new List<MemberBinding>();
        var target = Expression.Constant(null, targetType);
        foreach (var memberGroup in memberPaths.GroupBy(path => path[depth]))
        {
            var memberName = memberGroup.Key;
            var targetMember = Expression.PropertyOrField(target, memberName);
            var sourceMember = Expression.PropertyOrField(source, memberName);
            var childMembers = memberGroup.Where(path => depth + 1 < path.Length);
            var targetValue = !childMembers.Any() ? sourceMember :
                NewObject(targetMember.Type, sourceMember, childMembers, depth + 1);
            bindings.Add(Expression.Bind(targetMember.Member, targetValue));
        }
        return Expression.MemberInit(Expression.New(targetType), bindings);
    }

}