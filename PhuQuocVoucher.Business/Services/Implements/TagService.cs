using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class TagService : ServiceCrud<Tag>, ITagService
{
    public TagService(IUnitOfWork work) : base(work.Get<Tag>(), work)
    {
    }
}