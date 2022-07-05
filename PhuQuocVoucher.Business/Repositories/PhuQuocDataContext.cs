using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data;

public class PhuQuocDataContext : DbContext
{
    private readonly IConfiguration _config;

    public PhuQuocDataContext(IConfiguration configuration)
    {
        _config = configuration;

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config["ConnectionStrings:PhuQuocDB_devops"],
            b => b.MigrationsAssembly("PhuQuocVoucher.Api"));
    }


    public DbSet<User> Users { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Profile> Profiles { get; set; }

    public DbSet<Seller> Sellers { get; set; }

    public DbSet<ServiceProvider> Providers { get; set; }

    public DbSet<ProviderType> ProviderTypes { get; set; }

    public DbSet<Service> Services { get; set; }

    public DbSet<ServiceType> ServiceTypes { get; set; }

    public DbSet<Voucher>  Vouchers { get; set; }

    public DbSet<Combo> Combos { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Cart> Carts { get; set; }

    public DbSet<CartItem> CartItems { get; set; }

    public DbSet<Place> Places { get; set; }

    public DbSet<Blog> Blogs { get; set; }

    public DbSet<Tag> Tags { get; set; }
}