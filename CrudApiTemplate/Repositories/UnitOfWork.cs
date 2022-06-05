using Microsoft.EntityFrameworkCore;

namespace CrudApiTemplate.Repositories;

public class UnitOfWork : IUnitOfWork
{
    protected readonly DbContext DataContext;

    protected readonly Dictionary<Type, object> Repositories = new();

    public UnitOfWork(DbContext dataContext)
    {
        DataContext = dataContext;
    }

    public void Add<T>(IRepository<T> repository) where T : class
    {
        Repositories[typeof(T)] = repository;
    }

    public IRepository<T> Get<T>() where T : class
    {
        return (IRepository<T>) Repositories[typeof(T)];
    }

    public IRepository<object> Get(Type modelType)
    {
        return (IRepository<object>) Repositories[modelType];
    }

    public IRepository<TModel> Repository<TModel>() where TModel : class
    {
        return (Repositories[typeof(TModel)] as IRepository<TModel>)!;
    }


    public int Complete()
    {
        return DataContext.SaveChanges();
    }

    public virtual void Dispose()
    {
        DataContext.Dispose();
    }

}