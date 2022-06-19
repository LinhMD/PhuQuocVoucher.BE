using System.Linq.Expressions;
using CrudApiTemplate.View;

namespace CrudApiTemplate.Repository;

public interface IRepository<TModel> where TModel : class
{
    TModel? Get(int id);

    TView? Get<TView>(int id) where TView :class, IView<TModel>,  new();
    ValueTask<TModel?> GetAsync(int id);

    ValueTask<TView?> GetAsync<TView>(int id) where TView :class, IView<TModel>,  new();

    IQueryable<TModel> GetAll();

    IQueryable<TView> GetAll<TView>() where TView : class, IView<TModel>, new();

    IQueryable<TModel> Find(Expression<Func<TModel, bool>> predicate);

    IQueryable<TView> Find<TView>(Expression<Func<TModel, bool>> predicate) where TView : class, IView<TModel>, new();


    TModel Add(TModel model);
    void AddAll(IEnumerable<TModel> models);

    void Remove(TModel model);
    void RemoveAll(IEnumerable<TModel> models);

    public void Commit();

    ValueTask<TModel> AddAsync(TModel model);
    void AddAllAsync(IEnumerable<TModel> models);

    void RemoveAsync(TModel model);
    void RemoveAllAsync(IEnumerable<TModel> models);

    public Task CommitAsync();

    public IQueryable<TModel> IncludeAll();
}