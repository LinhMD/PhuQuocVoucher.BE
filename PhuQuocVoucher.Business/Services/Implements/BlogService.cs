using System.Reflection.Metadata;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.BlogDto;
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

    public async Task<BlogView> CreateBlogAsync(CreateBlog createBlog)
    {
        var blog = new Blog()
        {
            Content = createBlog.Content,
            Summary = createBlog.Summary,
            Status = createBlog.Status,
            BannerImage = createBlog.BannerImage,
            Title = createBlog.Title,
            CreateAt = DateTime.Now,
            
        };

        await Repository.AddAsync(blog);
        if (createBlog.PlaceIds != null)
        {
            var places = await UnitOfWork.Get<Place>().Find(p => createBlog.PlaceIds.Contains(p.Id)).ToListAsync();
            blog.Places = places;
        }
        
        if (createBlog.TagIds != null)
        {
            var tags = await UnitOfWork.Get<Tag>().Find(t => createBlog.TagIds.Contains(t.Id)).ToListAsync();
            blog.Tags = tags;
        }

        await UnitOfWork.CompleteAsync();
        
        return blog.Adapt<BlogView>();
    }
}