using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Repositories;

public class PhuQuocDataContext : DbContext
{
    private readonly IConfiguration _config;

    public PhuQuocDataContext(IConfiguration configuration)
    {
        _config = configuration;

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            _config["ConnectionStrings:PhuQuocDB_devops"],
            b => b.MigrationsAssembly("PhuQuocVoucher.Api")
                .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasOne(a => a.Cart)
            .WithOne(a => a.Customer)
            .HasForeignKey<Cart>(c => c.CustomerId);

        
        /*modelBuilder.Entity<OrderItem>().AfterInsert(trigger => trigger
            .Action(action => action
                .Update<Voucher>(
                    (item, voucher) => voucher.ProductId == item.OrderProductId && voucher.Inventory > 0, //Matching product id
                    (item, oldVoucher) => new Voucher {Inventory = oldVoucher.Inventory - 1})// reduce 1 inventory
            )
        );*/
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
    
    public DbSet<PriceLevel> PriceLevels { get; set; }
    
    public DbSet<PriceBook> PriceBooks { get; set; }
    
    public DbSet<QrCodeInfo> QrCodes { get; set; }
}