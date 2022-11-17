using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.BlogDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IBlogService : IServiceCrud<Blog>
{
    public Task<BlogView> CreateBlogAsync(CreateBlog createBlog);

    public Task<BlogView> UpdateBlogAsync(UpdateBlog updateBlog, int id);
}