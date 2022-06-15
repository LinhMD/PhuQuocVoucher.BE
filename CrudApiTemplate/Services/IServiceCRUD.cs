using CrudApiTemplate.Request;
using CrudApiTemplate.Utilities;
using CrudApiTemplate.View;

namespace CrudApiTemplate.Services
{
    public interface IServiceCrud<TModel> where TModel : class
    {
        TModel Get(int id);

        Task<TModel> GetAsync(int id);

        TView Get<TView>(int id) where TView : class, IView<TModel>, new();

        Task<TView> GetAsync<TView>(int id) where TView : class, IView<TModel>, new();

        IList<TModel> GetAll();

        Task<IList<TModel>> GetAllAsync();

        IList<TView> GetAll<TView>() where TView : class, IView<TModel>, new();
        Task<IList<TView>> GetAllAsync<TView>() where TView : class, IView<TModel>, new();

        IList<TModel> Find(IFindRequest<TModel> findRequest);

        Task<IList<TModel>> FindAsync(IFindRequest<TModel> findRequest);

        IList<TView> Find<TView>(IFindRequest<TModel> findRequest) where TView : class, IView<TModel>, new();
        Task<IList<TView>> FindAsync<TView>(IFindRequest<TModel> findRequest) where TView : class, IView<TModel>, new();

        (IList<TModel> models, int total) Get(GetRequest<TModel> getRequest);

        Task<(IList<TModel> models, int total)>  GetAsync(GetRequest<TModel> getRequest);


        (IList<TView> models, int total) Get<TView>(GetRequest<TModel> getRequest) where TView : class, IView<TModel>, new();
        Task<(IList<TView> models, int total)> GetAsync<TView>(GetRequest<TModel> getRequest) where TView : class, IView<TModel>, new();

        TModel Create(ICreateRequest<TModel> createRequest);

        Task<TModel> CreateAsync(ICreateRequest<TModel> createRequest);

        TModel Update(int id, IUpdateRequest<TModel> updateRequest);


        Task<TModel> UpdateAsync(int id, IUpdateRequest<TModel> updateRequest);

        TModel Delete(int id);

        Task<TModel> DeleteAsync(int id);

    }
}
