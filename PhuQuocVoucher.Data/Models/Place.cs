namespace PhuQuocVoucher.Data.Models;

public class Place
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string MapLocation { get; set; }

    public string Image { get; set; }

    public IEnumerable<Blog> Blogs { get; set; }

    public IEnumerable<Service> Services { get; set; }
}