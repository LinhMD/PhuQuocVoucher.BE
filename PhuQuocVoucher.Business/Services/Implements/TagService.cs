using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Tag> GetTagAsync(string name)
    {
        return await Repository.Find(t => t.Name == name).FirstOrDefaultAsync() ??
               await Repository.AddAsync(new Tag{Name = name, CreateAt = DateTime.Now});
    }

    public async Task<IList<Tag>> GetTagsAsync(IList<string> names)
    {
        var tags = await Repository.Find(t => names.Contains(t.Name)).ToListAsync();
        
        var foundTags = tags.Select(t => t.Name).ToList();
        
        var newTag = names.Except(foundTags).Select(t => new Tag {Name = t, CreateAt = DateTime.Now}).ToList();
        await Repository.AddAllAsync(newTag);
        tags.AddRange(newTag);
        return tags;
    }
}