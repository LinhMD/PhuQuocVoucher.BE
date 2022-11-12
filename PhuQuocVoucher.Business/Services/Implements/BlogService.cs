using System.Reflection.Metadata;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class BlogService : ServiceCrud<Blog>, IBlogService
{
    private ILogger _logger;
    public BlogService(IUnitOfWork work,  ILogger<BlogService> logger) : base(work.Get<Blog>(), work, logger)
    {
        _logger = logger;
    }
}