using CrudApiTemplate.Services;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface ITagService : IServiceCrud<Tag>
{
    public Task<Tag> GetTagAsync(string name);
    public Task<IList<Tag>> GetTagsAsync(IList<string> names);
}