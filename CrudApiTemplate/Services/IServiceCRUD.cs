using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using CrudApiTemplate.View;

namespace CrudApiTemplate.Services
{
    public interface IServiceCrud<TModel> where TModel : class
    {
        TModel Get(int id);

        TView Get<TView>(int id) where TView : class, IView<TModel>, new();

        IEnumerable<TModel> GetAll();

        IEnumerable<TView> GetAll<TView>() where TView : class, IView<TModel>, new();

        IEnumerable<TModel> Find(IFindRequest<TModel> findRequest);

        IEnumerable<TView> Find<TView>(IFindRequest<TModel> findRequest) where TView : class, IView<TModel>, new();

        (IEnumerable<TModel> models, int total) Get(GetRequest<TModel> getRequest);

        (IEnumerable<TView> models, int total) Get<TView>(GetRequest<TModel> getRequest) where TView : class, IView<TModel>, new();

        TModel Create(ICreateRequest<TModel> createRequest);

        TModel Update(int id, IUpdateRequest<TModel> updateRequest);

        TModel Delete(int id);


    }
}
