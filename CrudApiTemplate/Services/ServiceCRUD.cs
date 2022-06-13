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

    public ServiceCrud(IRepository<TModel> repository, IUnitOfWork work)
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



    public IEnumerable<TModel> Find(IFindRequest<TModel> findRequest)
    {
        return Repository.Find(findRequest.ToPredicate());
    }

    public IEnumerable<TView> Find<TView>(IFindRequest<TModel> findRequest) where TView : class, IView<TModel>, new()
    {
        return Repository.Find<TView>(findRequest.ToPredicate());
    }

    public (IEnumerable<TModel> models, int total) FindSortedPaging(IOrderRequest<TModel> orderRequest)
    {
        var result = Repository.Find(orderRequest.ToPredicate())
            .OrderBy(orderRequest)
            .Paging(orderRequest.GetPaging()).ToList();

        return (result, result.Count);
    }

    public (IEnumerable<TView> models, int total) FindSortedPaging<TView>(IOrderRequest<TModel> orderRequest) where TView : class, IView<TModel>, new()
    {

        var result = Repository.Find(orderRequest.ToPredicate())
            .OrderBy(orderRequest)
            .Paging(orderRequest.GetPaging())
            .ProjectToType<TView>().ToList();

        return (result, result.Count);
    }
    public TModel Get(int id)
    {
        var model = Repository.Get(id);

        if (model == null) throw new ModelNotFoundException<TModel>(typeof(TModel).Name);

        return model;
    }

    public IEnumerable<TModel> GetAll()
    {
        return Repository.GetAll();
    }

    public IEnumerable<TView> GetAll<TView>() where TView : class, IView<TModel>, new()
    {
        return Repository.GetAll<TView>();
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