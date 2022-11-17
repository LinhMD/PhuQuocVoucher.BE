
using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(PlaceId))]
[Index(nameof(BlogId))]
public class BlogPlace
{
    public int PlaceId { get; set; }

    public Place Place { get; set; }
    
    public int BlogId { get; set; }

    public Blog Blog { get; set; }
}