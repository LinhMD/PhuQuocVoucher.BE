using System.Reflection.Metadata;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Repositories;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class BlogService : ServiceCrud<Blog>, IBlogService
{
    public BlogService(PqUnitOfWork work) : base(work.Get<Blog>(), work)
    {
    }
}