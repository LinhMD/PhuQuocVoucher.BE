
namespace CrudApiTemplate.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IRepository<TModel>? Repository<TModel>() where TModel : class;
    public void Add<T>(IRepository<T> repository) where T : class;

    public IRepository<T> Get<T>() where T : class;

    public IRepository<object> Get(Type modelType);

    int Complete();
}