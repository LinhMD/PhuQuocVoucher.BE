using Microsoft.EntityFrameworkCore;

namespace CrudApiTemplate.Repository;

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
        if (!Repositories.ContainsKey(typeof(T)))
        {
            Repositories[typeof(T)] = new Repository<T>(DataContext);
        }
        return (IRepository<T>) Repositories[typeof(T)];
    }

    public IRepository<object> Get(Type modelType)
    {
        if (!Repositories.ContainsKey(modelType))
        {
            throw new Exception("Hey fuck you, add it your self");
        }
        return (IRepository<object>) Repositories[modelType];
    }


    public int Complete()
    {
        return DataContext.SaveChanges();
    }
    
    public async  Task<int> CompleteAsync()
    {
        return await DataContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        this.Repositories.Clear();
    }
}