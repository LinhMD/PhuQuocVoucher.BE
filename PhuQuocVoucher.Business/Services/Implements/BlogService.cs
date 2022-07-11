using System.Reflection.Metadata;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Repositories;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class BlogService : ServiceCrud<Blog>, IBlogService
{
    private ILogger<BlogService> _logger;
    public BlogService(IUnitOfWork work,  Logger<BlogService> logger) : base(work.Get<Blog>(), work, logger)
    {
        _logger = logger;
    }
}