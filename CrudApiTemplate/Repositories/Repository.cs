using System.Linq.Expressions;
using CrudApiTemplate.Request;
using CrudApiTemplate.View;
using Mapster;
using Microsoft.EntityFrameworkCore;
using CrudApiTemplate.Utilities;


namespace CrudApiTemplate.Repositories;

public class Repository<TModel> : IRepository<TModel> where TModel : class
{

    protected readonly DbContext Context;
    protected readonly DbSet<TModel> Models;

    public Repository(DbContext context)
    {
        Context = context;
        Models = context.Set<TModel>();
    }

    public TModel? Get(int id)
    {
        return Context.Set<TModel>().Find(id);
    }

    public async ValueTask<TModel?> GetAsync(int id)
    {
        return await Context.Set<TModel>().FindAsync(id);
    }

    public TView? Get<TView>(int id) where TView : class, IView<TModel>, new()
    {
        return Models.Find(id)?.Adapt<TView>();
    }

    public async ValueTask<TView?> GetAsync<TView>(int id) where TView : class, IView<TModel>, new()
    {
        var model = await Models.FindAsync(id);
        return model?.Adapt<TView>();
    }

    public IEnumerable<TModel> GetAll()
    {
        return WithAllData().ToList();
    }

    public async Task<IEnumerable<TModel>> GetAllAsync()
    {
        return await WithAllData().ToListAsync();
    }

    public IEnumerable<TView> GetAll<TView>() where TView : class, IView<TModel>, new()
    {
        return Models
            .ProjectToType<TView>()
            .ToList();
    }

    public async Task<IEnumerable<TView>> GetAllAsync<TView>() where TView : class, IView<TModel>, new()
    {
        return await Models
            .ProjectToType<TView>()
            .ToListAsync();
    }

    public (IEnumerable<TModel> models, int total) GetPaging(int page = 1, int pageSize = 20)
    {
        var total = WithAllData().Count();
        return (WithAllData()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList(), total);
    }

    public async Task<(IEnumerable<TModel> models, int total)> GetPagingAsync(int page = 1, int pageSize = 20)
    {
        int total = await WithAllData().CountAsync();

        var models = await WithAllData()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (models, total);
    }


    public (IEnumerable<TView> views, int total) GetPaging<TView>( int page = 1, int pageSize = 20)
        where TView : class, IView<TModel>, new()
    {

        var total = Models.Count();
        return (Models
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ProjectToType<TView>().ToList(), total);
    }

    public async Task<(IEnumerable<TView> views, int total)> GetPagingAsync<TView>(int page = 1, int pageSize = 20) where TView : class, IView<TModel>, new()
    {
        int total = await WithAllData().CountAsync();

        var models = await WithAllData()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ProjectToType<TView>()
            .ToListAsync();

        return (models, total);
    }

    public (IEnumerable<TModel> models, int total) GetOrderedPaging(IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20)
    {
        var models = WithAllData().OrderBy(orderBy);

        var total = models.Count();

        return (models.Skip((page - 1) * pageSize).Take(pageSize).ToList(), total);
    }

    public async Task<(IEnumerable<TModel> models, int total)> GetOrderedPagingAsync(IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20)
    {
        var models = WithAllData().OrderBy(orderBy);

        int total = await models.CountAsync();
        var result = await models.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return (result, total);
    }

    public (IEnumerable<TView> views, int total) GetOrderedPaging<TView>(IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20)
        where TView : class, IView<TModel>, new()
    {
        var views = Models
            .OrderBy(orderBy)
            .ProjectToType<TView>();

        var total = views.Count();

        return (views.Skip((page - 1) * pageSize).Take(pageSize).ToList(), total);
    }

    public async Task<(IEnumerable<TView> views, int total)> GetOrderedPagingAsync<TView>(IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20) where TView : class, IView<TModel>, new()
    {
        var views = Models
            .OrderBy(orderBy)
            .ProjectToType<TView>();
        var total = await views.CountAsync();
        return (await views.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(), total);

    }

    public IEnumerable<TModel> Find(Expression<Func<TModel, bool>> predicate)
    {
        return WithAllData()
            .Where(predicate)
            .ToList();

    }

    public async Task<IEnumerable<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate)
    {
        return await WithAllData()
            .Where(predicate)
            .ToListAsync();
    }

    public IEnumerable<TView> Find<TView>(Expression<Func<TModel, bool>> predicate)
        where TView : class, IView<TModel>, new()
    {
        return Models
            .Where(predicate)
            .ProjectToType<TView>()
            .ToList();
    }

    public async Task<IEnumerable<TView>> FindAsync<TView>(Expression<Func<TModel, bool>> predicate) where TView : class, IView<TModel>, new()
    {
        return await Models
            .Where(predicate)
            .ProjectToType<TView>()
            .ToListAsync();
    }

    public (IEnumerable<TModel> models, int total) FindOrderedPaging(Expression<Func<TModel, bool>> predicate, IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20)
    {
        var views = WithAllData()
            .Where(predicate)
            .OrderBy(orderBy);
        var total = views.Count();

        return (views.Skip((page - 1) * pageSize).Take(pageSize).ToList(), total);
    }

    public async Task<(IEnumerable<TModel> models, int total)> FindOrderedPagingAsync(Expression<Func<TModel, bool>> predicate, IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20)
    {
        var views = WithAllData()
            .Where(predicate)
            .OrderBy(orderBy);
        var total = await views.CountAsync();

        return (await views.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(), total);
    }

    public (IEnumerable<TView> views, int total) FindOrderedPaging<TView>(Expression<Func<TModel, bool>> predicate, IOrderRequest<TModel> orderBy,  int page = 1, int pageSize = 20)
        where TView : class, IView<TModel>, new()
    {
        var views = Models
            .Where(predicate)
            .OrderBy(orderBy)
            .ProjectToType<TView>();
        var total = views.Count();

        return (views.Skip((page - 1) * pageSize).Take(pageSize).ToList(), total);
    }

    public async Task<(IEnumerable<TView> views, int total)> FindOrderedPagingAsync<TView>(Expression<Func<TModel, bool>> predicate, IOrderRequest<TModel> orderBy, int page = 1, int pageSize = 20) where TView : class, IView<TModel>, new()
    {
        var views = WithAllData()
            .Where(predicate)
            .OrderBy(orderBy)
            .ProjectToType<TView>();
        int total = await views.CountAsync();

        return (await views.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(), total);
    }

    public TModel Add(TModel model)
    {
        Models.Add(model);
        Context.SaveChanges();
        Context.Entry(model).GetDatabaseValues();
        return model;
    }

    public void AddAll(IEnumerable<TModel> models)
    {
        Models.AddRange(models);
        Context.SaveChanges();
    }

    public void Remove(TModel model)
    {
        Models.Remove(model);

        Context.SaveChanges();
    }

    public void RemoveAll(IEnumerable<TModel> models)
    {
        Models.RemoveRange(models);
        Context.SaveChanges();
    }


    public void Commit()
    {
        Context.SaveChanges();
    }

    public async ValueTask<TModel> AddAsync(TModel model)
    {
        await Models.AddAsync(model);
        await Context.SaveChangesAsync();
        await Context.Entry(model).GetDatabaseValuesAsync();
        return model;
    }

    public async void AddAllAsync(IEnumerable<TModel> models)
    {
        await Models.AddRangeAsync(models);
        await Context.SaveChangesAsync();
    }

    public async void RemoveAsync(TModel model)
    {
        Models.Remove(model);

        await Context.SaveChangesAsync();
    }

    public async void RemoveAllAsync(IEnumerable<TModel> models)
    {
        Models.RemoveRange(models);

        await Context.SaveChangesAsync();
    }

    public async void CommitAsync()
    {
        await Context.SaveChangesAsync();
    }


    public virtual IQueryable<TModel> WithAllData()
    {
        return this.Models.AsQueryable();
    }

}