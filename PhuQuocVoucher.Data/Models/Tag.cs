using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(Name), IsUnique = true)]
public class Tag : BaseModel
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public IList<Blog> Blogs { get; set; }
    
    public IList<Product> Products { get; set; }
}