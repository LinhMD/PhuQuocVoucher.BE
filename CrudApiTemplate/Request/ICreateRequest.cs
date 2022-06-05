using CrudApiTemplate.Attributes;
using CrudApiTemplate.Repositories;
using Mapster;

namespace CrudApiTemplate.Request;

public interface ICreateRequest<TModel> where TModel: class
{
    virtual TModel CreateNew(IUnitOfWork work)
    {
        /*foreach (var requestProperties in this.GetType().GetProperties())
        {
            var propertyValue = requestProperties.GetValue(this);
            if(propertyValue is null) continue;

            var modelPathAttribute = Attribute.GetCustomAttribute(requestProperties, typeof(ModelPathAttribute)) as ModelPathAttribute;

        }*/
        return this.Adapt<TModel>();
    }
}