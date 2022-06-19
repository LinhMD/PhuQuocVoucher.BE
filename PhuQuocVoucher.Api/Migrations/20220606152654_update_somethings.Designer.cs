﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhuQuocVoucher.Data;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    [DbContext(typeof(PhuQuocDataContext))]
    [Migration("20220606152654_update_somethings")]
    partial class update_somethings
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BlogPlace", b =>
                {
                    b.Property<int>("BlogsId")
                        .HasColumnType("int");

                    b.Property<int>("PlacesId")
                        .HasColumnType("int");

                    b.HasKey("BlogsId", "PlacesId");

                    b.HasIndex("PlacesId");

                    b.ToTable("BlogPlace");
                });

            modelBuilder.Entity("ComboVoucher", b =>
                {
                    b.Property<int>("CombosId")
                        .HasColumnType("int");

                    b.Property<int>("VouchersId")
                        .HasColumnType("int");

                    b.HasKey("CombosId", "VouchersId");

                    b.HasIndex("VouchersId");

                    b.ToTable("ComboVoucher");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BannerImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Combo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Combos");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("UserInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId")
                        .IsUnique()
                        .HasFilter("[UserInfoId] IS NOT NULL");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CompleteDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("SellerId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("SellerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("OrderProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("OrderProductId");

                    b.HasIndex("ProfileId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MapLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AssignSellerId")
                        .HasColumnType("int");

                    b.Property<string>("BannerImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripsion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsForKid")
                        .HasColumnType("bit");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CivilIdentify")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.ProviderType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProviderTypes");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("OrderItemId")
                        .HasColumnType("int");

                    b.Property<byte>("Rating")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderItemId")
                        .IsUnique();

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("CommissionRate")
                        .HasColumnType("real");

                    b.Property<string>("SellerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId")
                        .IsUnique();

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceLocationId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.HasIndex("ServiceLocationId");

                    b.HasIndex("TypeId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.ServiceProvider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AssignedSellerId")
                        .HasColumnType("int");

                    b.Property<string>("ProviderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TaxCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssignedSellerId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserInfoId")
                        .IsUnique();

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.ServiceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ParentTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentTypeId");

                    b.ToTable("ServiceTypes");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BlogId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AvatarLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FireBaseUid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Voucher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Inventory")
                        .HasColumnType("int");

                    b.Property<bool>("IsRequireProfileInfo")
                        .HasColumnType("bit");

                    b.Property<int>("LimitPerDay")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("VoucherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.HasIndex("ServiceId");

                    b.HasIndex("VoucherName")
                        .IsUnique();

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("BlogPlace", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.Blog", null)
                        .WithMany()
                        .HasForeignKey("BlogsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhuQuocVoucher.Data.Models.Place", null)
                        .WithMany()
                        .HasForeignKey("PlacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ComboVoucher", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.Combo", null)
                        .WithMany()
                        .HasForeignKey("CombosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhuQuocVoucher.Data.Models.Voucher", null)
                        .WithMany()
                        .HasForeignKey("VouchersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Cart", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.CartItem", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.Cart", null)
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhuQuocVoucher.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Combo", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Customer", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.User", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Order", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("PhuQuocVoucher.Data.Models.Seller", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId");

                    b.Navigation("Customer");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.OrderItem", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhuQuocVoucher.Data.Models.Product", "OrderProduct")
                        .WithMany()
                        .HasForeignKey("OrderProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhuQuocVoucher.Data.Models.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");

                    b.Navigation("Order");

                    b.Navigation("OrderProduct");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Profile", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.Customer", "Customer")
                        .WithMany("Profiles")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Review", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.Customer", null)
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerId");

                    b.HasOne("PhuQuocVoucher.Data.Models.OrderItem", "OrderItem")
                        .WithOne("Review")
                        .HasForeignKey("PhuQuocVoucher.Data.Models.Review", "OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Seller", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.User", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Service", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.ServiceProvider", "Provider")
                        .WithMany("Services")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhuQuocVoucher.Data.Models.Place", "ServiceLocation")
                        .WithMany("Services")
                        .HasForeignKey("ServiceLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhuQuocVoucher.Data.Models.ServiceType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");

                    b.Navigation("ServiceLocation");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.ServiceProvider", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.Seller", "AssignedSeller")
                        .WithMany()
                        .HasForeignKey("AssignedSellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhuQuocVoucher.Data.Models.ProviderType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhuQuocVoucher.Data.Models.User", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedSeller");

                    b.Navigation("Type");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.ServiceType", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.ServiceType", "ParentType")
                        .WithMany()
                        .HasForeignKey("ParentTypeId");

                    b.Navigation("ParentType");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Tag", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.Blog", null)
                        .WithMany("Tags")
                        .HasForeignKey("BlogId");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Voucher", b =>
                {
                    b.HasOne("PhuQuocVoucher.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhuQuocVoucher.Data.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Blog", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Profiles");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.OrderItem", b =>
                {
                    b.Navigation("Review");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.Place", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("PhuQuocVoucher.Data.Models.ServiceProvider", b =>
                {
                    b.Navigation("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
