using System.Linq.Expressions;
using CrudApiTemplate.View;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CrudApiTemplate.Repository;

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
        return Models.Find(id);
    }

    public async ValueTask<TModel?> GetAsync(int id)
    {
        var model = await Models.FindAsync(id);
        return model;
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

    public IQueryable<TModel> GetAll()
    {
        return IncludeAll();
    }

    public IQueryable<TView> GetAll<TView>() where TView : class, IView<TModel>, new()
    {
        return Models.ProjectToType<TView>();
    }

    public IQueryable<TModel> Find(Expression<Func<TModel, bool>> predicate)
    {
        return IncludeAll().Where(predicate);
    }

    public IQueryable<TView> Find<TView>(Expression<Func<TModel, bool>> predicate)
        where TView : class, IView<TModel>, new()
    {
        return Models
            .Where(predicate)
            .ProjectToType<TView>();
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

    public async Task CommitAsync()
    {
        await Context.SaveChangesAsync();
    }

    public virtual IQueryable<TModel> IncludeAll()
    {
        return Models.AsQueryable();
    }
}