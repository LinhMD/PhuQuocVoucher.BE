using System.Reflection.Metadata;
using CrudApiTemplate.CustomException;
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
            blog.Places = places.Select( p => new BlogPlace(){ Place = p, PlaceId = p.Id, BlogId = blog.Id}).ToList();
        }
        
        if (createBlog.TagIds != null)
        {
            var tags = await UnitOfWork.Get<Tag>().Find(t => createBlog.TagIds.Contains(t.Id)).ToListAsync();
            blog.Tags = tags.Select( p => new BlogTag(){ TagId = p.Id, BlogId = blog.Id}).ToList();
        }

        await UnitOfWork.CompleteAsync();
        
        return blog.Adapt<BlogView>();
    }

    public async Task<BlogView> UpdateBlogAsync(UpdateBlog updateBlog, int id)
    {
        var blog = await Repository.Find(b => b.Id == id).FirstOrDefaultAsync();
        if (blog == null) throw new ModelNotFoundException($"not found blog with id {id}");
        if (updateBlog.Content != null)
        {
            blog.Content = updateBlog.Content;
        }
        
        if (updateBlog.Summary != null)
        {
            blog.Summary = updateBlog.Summary;
        }
        
        if (updateBlog.Title != null)
        {
            blog.Title = updateBlog.Title;
        }
        
        if (updateBlog.BannerImage != null)
        {
            blog.BannerImage = updateBlog.BannerImage;
        }
        if (updateBlog.Status != null)
        {
            blog.Status = updateBlog.Status ?? ModelStatus.Active;
        }
        if (updateBlog.PlaceIds != null)
        {
            await UnitOfWork.Get<BlogPlace>().RemoveAllAsync(blog.Places);
            var places = await UnitOfWork.Get<Place>().Find(p => updateBlog.PlaceIds.Contains(p.Id)).ToListAsync();
            blog.Places = places.Select( p => new BlogPlace(){ Place = p, PlaceId = p.Id, BlogId = blog.Id}).ToList();
        }
        
        if (updateBlog.TagIds != null)
        {
            await UnitOfWork.Get<BlogTag>().RemoveAllAsync(blog.Tags);
            var tags = await UnitOfWork.Get<Tag>().Find(t => updateBlog.TagIds.Contains(t.Id)).ToListAsync();
            blog.Tags = tags.Select( p => new BlogTag(){ TagId = p.Id, BlogId = blog.Id}).ToList();
        }

        await UnitOfWork.CompleteAsync();
        return blog.Adapt<BlogView>();
    }
}