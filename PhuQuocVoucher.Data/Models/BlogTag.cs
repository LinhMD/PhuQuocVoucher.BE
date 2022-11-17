using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(TagId))]
[Index(nameof(BlogId))]
public class BlogTag
{
    public int Id { get; set; }

    public int TagId { get; set; }

    public Tag Tag { get; set; }
    
    public int BlogId { get; set; }

    public Blog Blog { get; set; }
}