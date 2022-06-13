using CrudApiTemplate.Attributes;
using CrudApiTemplate.Repositories;
using Mapster;

namespace CrudApiTemplate.Request;

public interface ICreateRequest<TModel> where TModel: class
{
    virtual TModel CreateNew(IUnitOfWork work)
    {
        return this.Adapt<TModel>();
    }
}