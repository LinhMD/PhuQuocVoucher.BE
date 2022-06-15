using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using CrudApiTemplate.View;

namespace CrudApiTemplate.Services
{
    public interface IServiceCrud<TModel> where TModel : class
    {
        TModel Get(int id);

        TView Get<TView>(int id) where TView : class, IView<TModel>, new();

        IList<TModel> GetAll();

        IList<TView> GetAll<TView>() where TView : class, IView<TModel>, new();

        IList<TModel> Find(IFindRequest<TModel> findRequest);

        IList<TView> Find<TView>(IFindRequest<TModel> findRequest) where TView : class, IView<TModel>, new();

        (IList<TModel> models, int total) Get(GetRequest<TModel> getRequest);

        (IList<TView> models, int total) Get<TView>(GetRequest<TModel> getRequest) where TView : class, IView<TModel>, new();

        TModel Create(ICreateRequest<TModel> createRequest);

        TModel Update(int id, IUpdateRequest<TModel> updateRequest);

        TModel Delete(int id);


    }
}
