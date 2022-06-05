using System.Linq.Expressions;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using CrudApiTemplate.View;

namespace CrudApiTemplate.Repositories;

public interface IRepository<TModel> where TModel : class
{
    TModel? Get(int id);

    ValueTask<TModel?> GetAsync(int id);

    TView? Get<TView>(int id) where TView :class, IView<TModel>,  new();

    ValueTask<TView?> GetAsync<TView>(int id) where TView :class, IView<TModel>,  new();

    IEnumerable<TModel> GetAll();
    Task<IEnumerable<TModel>> GetAllAsync();

    IEnumerable<TView> GetAll<TView>() where TView : class, IView<TModel>, new();

    Task<IEnumerable<TView>> GetAllAsync<TView>() where TView : class, IView<TModel>, new();

    (IEnumerable<TModel> models, int total) GetPaging(int page = 1, int pageSize = 20);
    Task<(IEnumerable<TModel> models, int total)> GetPagingAsync(int page = 1, int pageSize = 20);

    (IEnumerable<TView> views, int total)  GetPaging<TView>(int page = 1, int pageSize = 20) where TView : class, IView<TModel>, new();

    Task<(IEnumerable<TView> views, int total)> GetPagingAsync<TView>(int page = 1, int pageSize = 20) where TView : class, IView<TModel>, new();

    (IEnumerable<TModel> models, int total) GetOrderedPaging(IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20);

    Task<(IEnumerable<TModel> models, int total)> GetOrderedPagingAsync(IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20);

    (IEnumerable<TView> views, int total) GetOrderedPaging<TView>(IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20) where TView : class, IView<TModel>, new();

    Task<(IEnumerable<TView> views, int total)> GetOrderedPagingAsync<TView>(IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20) where TView : class, IView<TModel>, new();

    IEnumerable<TModel> Find(Expression<Func<TModel, bool>> predicate);

    Task<IEnumerable<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate);

    IEnumerable<TView> Find<TView>(Expression<Func<TModel, bool>> predicate) where TView : class, IView<TModel>, new();

    Task<IEnumerable<TView>> FindAsync<TView>(Expression<Func<TModel, bool>> predicate) where TView : class, IView<TModel>, new();

    (IEnumerable<TModel> models, int total) FindOrderedPaging(Expression<Func<TModel, bool>> predicate, IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20);

    Task<(IEnumerable<TModel> models, int total)> FindOrderedPagingAsync(Expression<Func<TModel, bool>> predicate, IOrderRequest<TModel> orderBy,  int page = 1, int pageSize = 20);

    (IEnumerable<TView> views, int total) FindOrderedPaging<TView>(Expression<Func<TModel, bool>> predicate, IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20) where TView : class, IView<TModel>, new();

    Task<(IEnumerable<TView> views, int total)> FindOrderedPagingAsync<TView>(Expression<Func<TModel, bool>> predicate, IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20) where TView : class, IView<TModel>, new();


    IQueryable<TModel> WithAllData();

    TModel Add(TModel model);
    void AddAll(IEnumerable<TModel> models);

    void Remove(TModel model);
    void RemoveAll(IEnumerable<TModel> models);

    public void Commit();

    ValueTask<TModel> AddAsync(TModel model);
    void AddAllAsync(IEnumerable<TModel> models);

    void RemoveAsync(TModel model);
    void RemoveAllAsync(IEnumerable<TModel> models);

    public void CommitAsync();
}