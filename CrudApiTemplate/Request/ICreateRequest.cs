using CrudApiTemplate.Repository;
using Mapster;

namespace CrudApiTemplate.Request;

public interface ICreateRequest<out TModel> where TModel: class
{
    virtual TModel CreateNew(IUnitOfWork work)
    {
        return this.Adapt<TModel>();
    }
}