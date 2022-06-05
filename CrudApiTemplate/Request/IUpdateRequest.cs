using CrudApiTemplate.Repositories;

namespace CrudApiTemplate.Request;

public interface IUpdateRequest<TModel> where TModel : class
{
    public bool UpdateModel(ref TModel model, IUnitOfWork work);
}