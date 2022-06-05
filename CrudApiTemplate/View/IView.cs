using CrudApiTemplate.Attributes;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CrudApiTemplate.View;

public interface IView<TModel> where TModel : class
{
    /*public IQueryable<TModel> DynamicInclude(IQueryable<TModel> dbSet)
    {
        foreach (var path in GetNavigatePaths(GetType()))
        {
            dbSet.Include(path);
        }
        return dbSet;
    }

    public static List<string> GetNavigatePaths(Type viewType)
    {
        var customAttributes = viewType.GetCustomAttributes(typeof(IncludeAttribute), true);
        return customAttributes.Cast<IncludeAttribute>().Select(attribute => attribute.Path).ToList();
    }*/


}