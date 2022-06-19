using CrudApiTemplate.Repository;

namespace CrudApiTemplate.Request;

public interface IUpdateRequest<TModel> where TModel : class
{
    public virtual bool UpdateModel(ref TModel model, IUnitOfWork work)
    {
        foreach (var updateProperty in GetType().GetProperties())
        {
            var modelProperty = typeof(TModel).GetProperty(updateProperty.Name);
            var updateValue = updateProperty.GetValue(this);
            if(updateValue is not null)
                modelProperty?.SetValue(model, updateValue);
        }
        return true;
    }
}