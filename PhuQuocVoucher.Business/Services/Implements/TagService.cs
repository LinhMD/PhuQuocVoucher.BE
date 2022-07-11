using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class TagService : ServiceCrud<Tag>, ITagService
{
    private ILogger<TagService> _logger;
    public TagService(IUnitOfWork work, ILogger<TagService> logger) : base(work.Get<Tag>(), work, logger)
    {
        _logger = logger;
    }
}