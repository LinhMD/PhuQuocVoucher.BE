using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using CrudApiTemplate.View;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CrudApiTemplate.Services;

public abstract class ServiceCrud<TModel> : IServiceCrud<TModel> where TModel : class
{
    protected readonly IRepository<TModel> Repository;
    protected readonly IUnitOfWork UnitOfWork;

    protected readonly ILogger<ServiceCrud<TModel>> Logger;

    protected ServiceCrud(IRepository<TModel> repository, IUnitOfWork work, ILogger<ServiceCrud<TModel>> logger)
    {
        Repository = repository;
        UnitOfWork = work;
        Logger = logger;
    }

    public TModel Create(ICreateRequest<TModel> createRequest)
    {
        var model = createRequest.CreateNew(UnitOfWork).Validate();
        try
        {
            model = Repository.Add(model);
        }
        catch(Exception ex)
        {
            var message = $"Create {typeof(TModel).Name} failed with message: {ex.Message}";
            Logger.LogError(ex, message);
            throw new DbQueryException(message, DbError.Create);
        }
        return model;
    }

    public async Task<TModel> CreateAsync(ICreateRequest<TModel> createRequest)
    {
        var model = createRequest.CreateNew(UnitOfWork).Validate();

        try
        {
            model = await Repository.AddAsync(model);
        }
        catch(DbUpdateException e)
        {
            var exceptionMessage = e.InnerException?.Message ?? "";
            Logger.LogError(e.InnerException, exceptionMessage);
            if (exceptionMessage.Contains("duplicate"))
            {
                var dupValue = exceptionMessage.Substring(exceptionMessage.IndexOf('(') + 1,
                    (exceptionMessage.IndexOf(')') - exceptionMessage.IndexOf('(') - 1));
                throw new DbQueryException($"Duplicate value: {dupValue}", DbError.Create);
            }
            throw new DbQueryException($"Error create {typeof(TModel).Name}  with message: {exceptionMessage}.", DbError.Create);
        }
        return model;
    }

    public TModel Update(int id, IUpdateRequest<TModel> updateRequest)
    {
        var model = Get(id);
        updateRequest.UpdateModel(ref model, UnitOfWork);
        model.Validate();
        try
        {
            Repository.Commit();
        }
        catch(Exception ex)
        {
            var mess = $"Update {typeof(TModel).Name} id: {id}, failed with message: {ex.Message}";
            Logger.LogError(ex, mess);
            throw new DbQueryException(mess, DbError.Update);
        }
        return model;
    }
    
    public async Task<TModel> UpdateAsync(int id, IUpdateRequest<TModel> updateRequest)
    {
        var model = await GetAsync(id);

        updateRequest.UpdateModel(ref model, UnitOfWork);
        model.Validate();
        try
        {
            await Repository.CommitAsync();
        }
        catch(Exception ex)
        {
            var message = $"Update {typeof(TModel).Name} id: {id}, failed with message: {ex.Message}";
            Logger.LogError(ex, message);
            throw new DbQueryException(message, DbError.Update);
        }

        return model;
    }

    public TModel Delete(int id)
    {
        var model = Get(id);
        try
        {
            Repository.Remove(model);
        }
        catch (Exception ex)
        {
            var mess = $"Delete {typeof(TModel).Name} id: {id}, failed with message: {ex.Message}";
            Logger.LogError(ex, mess);
            throw new DbQueryException(mess, DbError.Delete);
        }
        return model;
    }

    public async Task<TModel> DeleteAsync(int id)
    {
        var model = await GetAsync(id);

        try
        {
            await Repository.RemoveAsync(model);
        }
        catch (Exception ex)
        {
            var message = $"Delete {typeof(TModel).Name} id: {id}, failed with message: {ex.Message}";
            Logger.LogError(ex, message);
            throw new DbQueryException(message, DbError.Delete);
        }

        return model;
    }

    public TModel Get(int id)
    {
        var model = Repository.Get(id);

        if (model == null) throw new ModelNotFoundException($"Not Found {typeof(TModel).Name} with id {id}");

        return model;
    }
    public async Task<TModel> GetAsync(int id)
    {
        var model = await Repository.GetAsync(id);

        if (model == null) throw new ModelNotFoundException($"Not Found {typeof(TModel).Name} with id {id}");

        return model;
    }

    public TView Get<TView>(int id) where TView : class, IView<TModel>, new()
    {
        var view = Repository.Get<TView>(id);

        if (view == null) throw new ModelNotFoundException($"Not Found {typeof(TModel).Name} with id {id}");

        return view;
    }

    public async Task<TView> GetAsync<TView>(int id) where TView : class, IView<TModel>, new()
    {
        var view = await Repository.GetAsync<TView>(id);

        if (view == null) throw new ModelNotFoundException($"Not Found {typeof(TModel).Name} with id {id}");

        return view;
    }


    public IList<TModel> Find(IFindRequest<TModel> findRequest)
    {
        return Repository.Find(findRequest.ToPredicate()).ToList();
    }

    public async Task<IList<TModel>> FindAsync(IFindRequest<TModel> findRequest)
    {
        return await Repository.Find(findRequest.ToPredicate()).ToListAsync();
    }

    public IList<TView> Find<TView>(IFindRequest<TModel> findRequest) where TView : class, IView<TModel>, new()
    {
        return Repository.Find<TView>(findRequest.ToPredicate()).ToList();
    }

    public async Task<IList<TView>> FindAsync<TView>(IFindRequest<TModel> findRequest) where TView : class, IView<TModel>, new()
    {
        return await Repository.Find<TView>(findRequest.ToPredicate()).ToListAsync();
    }

    public (IList<TModel> models, int total) Get(GetRequest<TModel> getRequest)
    {
        var filter = Repository.Find(getRequest.FindRequest.ToPredicate());
        var total = filter.Count();
        var result = default(IList<TModel>);
        try
        {
            result = filter
                .OrderBy(getRequest.OrderRequest)
                .Paging(getRequest.GetPaging()).ToList();
        }
        catch (Exception e)
        {
            result = filter.Paging(getRequest.GetPaging()).ToList();

        }
        

        return (result, total);
    }


    public async Task<(IList<TModel> models, int total)> GetAsync(GetRequest<TModel> getRequest)
    {

        var filter = Repository.Find(getRequest.FindRequest.ToPredicate());

        var total = await filter.CountAsync();

        var result = default(IList<TModel>);
        try
        {
            result = await filter
                .OrderBy(getRequest.OrderRequest)
                .Paging(getRequest.GetPaging()).ToListAsync();
        }
        catch (Exception e)
        {
            result = await filter.Paging(getRequest.GetPaging()).ToListAsync();

        }

        return (result, total);
    }

    public (IList<TView> models, int total) Get<TView>(GetRequest<TModel> getRequest) where TView : class, IView<TModel>, new()
    {
        var queryable = Repository.Find(getRequest.FindRequest.ToPredicate());

        var total = queryable.Count();

        /*var result = queryable
            .OrderBy(getRequest.OrderRequest)
            .Paging(getRequest.GetPaging())
            .ProjectToType<TView>().ToList();*/
        var result = default(IList<TView>);
        try
        {
            result = queryable
                .OrderBy(getRequest.OrderRequest)
                .Paging(getRequest.GetPaging())
                .ProjectToType<TView>()
                .ToList();
        }
        catch (Exception e)
        {
            result = queryable.Paging(getRequest.GetPaging()) .ProjectToType<TView>().ToList();
        }
        return (result, total);
    }

    public async Task<(IList<TView> models, int total)> GetAsync<TView>(GetRequest<TModel> getRequest) where TView : class, IView<TModel>, new()
    {
        var filter = Repository.Find(getRequest.FindRequest.ToPredicate());
        var total = await filter.CountAsync();
        var result = default(IList<TView>);
        try
        {
            result = await filter
                .OrderBy(getRequest.OrderRequest)
                .Paging(getRequest.GetPaging())
                .ProjectToType<TView>()
                .ToListAsync();
        }
        catch (Exception e)
        {
            result = await filter.Paging(getRequest.GetPaging()) .ProjectToType<TView>().ToListAsync();
        }
        return (result, total);
    }

    public IList<TModel> GetAll()
    {
        return Repository.GetAll().ToList();
    }

    public async Task<IList<TModel>> GetAllAsync()
    {
        return await Repository.GetAll().ToListAsync();
    }

    public IList<TView> GetAll<TView>() where TView : class, IView<TModel>, new()
    {
        return Repository.GetAll<TView>().ToList();
    }

    public async Task<IList<TView>> GetAllAsync<TView>() where TView : class, IView<TModel>, new()
    {
        return await Repository.GetAll<TView>().ToListAsync();
    }


}