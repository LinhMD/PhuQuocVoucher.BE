﻿using Microsoft.EntityFrameworkCore;
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

        modelBuilder.Entity<Order>()
            .HasOne(o => o.PaymentDetail)
            .WithOne(p => p.Order)
            .HasForeignKey<PaymentDetail>(p => p.OrderId);

        modelBuilder.Entity<ComboVoucher>()
            .HasOne(cp => cp.Combo)
            .WithMany(voucher => voucher.Vouchers)
            .HasForeignKey(combo => combo.ComboId);
            
        modelBuilder.Entity<ComboVoucher>()
            .HasOne(cp => cp.Voucher)
            .WithMany(voucher => voucher.Combos)
            .HasForeignKey(combo => combo.VoucherId);
        
        modelBuilder.Entity<ComboVoucher>()
            .HasKey(c => new {voucherId = c.VoucherId, c.ComboId });
        
        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        modelBuilder.Entity<BlogPlace>()
            .HasKey(c => new { c.BlogId, c.PlaceId });
        
        modelBuilder.Entity<BlogTag>()
            .HasKey(c => new { c.BlogId, c.TagId });
        
        modelBuilder.Entity<VoucherTag>()
            
            .HasKey(c => new {TagsId = c.TagId, VouchersId = c.VoucherId });
        
    }
    
    
    public DbSet<VoucherTag> VoucherTags { get; set; }
    
    public DbSet<User> Users { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Seller> Sellers { get; set; }

    public DbSet<ServiceProvider> Providers { get; set; }

    public DbSet<Service> Services { get; set; }

    public DbSet<ServiceType> ServiceTypes { get; set; }

    public DbSet<Voucher>  Vouchers { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Cart> Carts { get; set; }

    public DbSet<CartItem> CartItems { get; set; }

    public DbSet<Place> Places { get; set; }

    public DbSet<Blog> Blogs { get; set; }
    
    public DbSet<BlogTag> BlogTag { get; set; }

    public DbSet<BlogPlace> BlogPlace { get; set; }

    public DbSet<Tag> Tags { get; set; }
    
    public DbSet<QrCode> QrCodes { get; set; }
    
    public DbSet<PaymentDetail> PaymentDetails { get; set; }
    
    public DbSet<ComboVoucher> VoucherCombos { get; set; }

    public DbSet<SellerActivity> Activities { get; set; }
    
    
}