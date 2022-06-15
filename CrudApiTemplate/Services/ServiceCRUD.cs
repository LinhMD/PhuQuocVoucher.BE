using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repositories;
using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using CrudApiTemplate.View;
using Mapster;

namespace CrudApiTemplate.Services;

public abstract class ServiceCrud<TModel> : IServiceCrud<TModel> where TModel : class
{
    protected readonly IRepository<TModel> Repository;
    protected readonly IUnitOfWork UnitOfWork;

    protected ServiceCrud(IRepository<TModel> repository, IUnitOfWork work)
    {
        Repository = repository;
        UnitOfWork = work;
    }



    public TModel Create(ICreateRequest<TModel> createRequest)
    {
        var model = createRequest.CreateNew(UnitOfWork);

        model.Validate();

        try
        {
            model = Repository.Add(model);
        }
        catch(Exception ex)
        {
            throw new DbQueryException(ex);
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
            throw new DbQueryException(ex);
        }

        return model;
    }

    public TView Get<TView>(int id) where TView : class, IView<TModel>, new()
    {
        var view = Repository.Get<TView>(id);

        if (view == null) throw new ModelNotFoundException<TModel>(typeof(TModel).Name);

        return view;
    }



    public IList<TModel> Find(IFindRequest<TModel> findRequest)
    {
        return Repository.Find(findRequest.ToPredicate()).ToList();
    }

    public IList<TView> Find<TView>(IFindRequest<TModel> findRequest) where TView : class, IView<TModel>, new()
    {
        return Repository.Find<TView>(findRequest.ToPredicate()).ToList();
    }

    public (IList<TModel> models, int total) Get(GetRequest<TModel> getRequest)
    {
        var filter = Repository.Find(getRequest.FindRequest.ToPredicate());
        var total = filter.Count();
        var result = filter
            .OrderBy(getRequest.OrderRequest)
            .Paging(getRequest.GetPaging()).ToList();

        return (result, total);
    }

    public (IList<TView> models, int total) Get<TView>(GetRequest<TModel> getRequest) where TView : class, IView<TModel>, new()
    {
        var queryable = Repository.Find(getRequest.FindRequest.ToPredicate());

        var total = queryable.Count();

        var result = queryable
            .OrderBy(getRequest.OrderRequest)
            .Paging(getRequest.GetPaging())
            .ProjectToType<TView>().ToList();

        return (result, total);
    }
    public TModel Get(int id)
    {
        var model = Repository.Get(id);

        if (model == null) throw new ModelNotFoundException<TModel>(typeof(TModel).Name);

        return model;
    }

    public IList<TModel> GetAll()
    {
        return Repository.GetAll().ToList();
    }

    public IList<TView> GetAll<TView>() where TView : class, IView<TModel>, new()
    {
        return Repository.GetAll<TView>().ToList();
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
            throw new DbQueryException(ex);
        }

        return model;
    }
}