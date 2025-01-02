namespace ThMoCo.Api.Data;

using Microsoft.EntityFrameworkCore;
using ThMoCo.Api.DTO;
/// <summary>
/// dotnet ef migrations add WhatMigrationFor --context AppDbContext --output-dir Migrations
/// dotnet ef database update
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<ProductDTO> Products { get; set; }
    public DbSet<PaymentCard> PaymentCards { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure AppUser relationships
        modelBuilder.Entity<AppUser>()
            .HasOne(u => u.PaymentCard)
            .WithOne()
            .HasForeignKey<AppUser>(u => u.PaymentCardId);

        modelBuilder.Entity<AppUser>()
            .HasOne(u => u.Address)
            .WithOne()
            .HasForeignKey<AppUser>(u => u.AddressId);

        modelBuilder.Entity<ProductDTO>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
        });
        // Seed data
        modelBuilder.Entity<ProductDTO>().HasData(
            new ProductDTO { Id = 1, Name = "Laptop", Price = 999.99m, Category = "Electronics", StockQuantity = 10, IsAvailable = true, ImageUrl = "http://example.com/laptop.jpg", CreatedDate = DateTime.Now.AddMonths(-6), UpdatedDate = DateTime.Now, Description = "A high-performance laptop for work and gaming." },
            new ProductDTO { Id = 2, Name = "Smartphone", Price = 799.99m, Category = "Electronics", StockQuantity = 20, IsAvailable = true, ImageUrl = "http://example.com/smartphone.jpg", CreatedDate = DateTime.Now.AddMonths(-3), UpdatedDate = DateTime.Now, Description = "A modern smartphone with excellent camera quality." },
            new ProductDTO { Id = 3, Name = "Headphones", Price = 199.99m, Category = "Accessories", StockQuantity = 50, IsAvailable = true, ImageUrl = "http://example.com/headphones.jpg", CreatedDate = DateTime.Now.AddMonths(-1), UpdatedDate = DateTime.Now, Description = "Noise-canceling headphones for immersive sound." },
            new ProductDTO { Id = 4, Name = "Monitor", Price = 299.99m, Category = "Electronics", StockQuantity = 5, IsAvailable = true, ImageUrl = "http://example.com/monitor.jpg", CreatedDate = DateTime.Now.AddMonths(-2), UpdatedDate = DateTime.Now, Description = "A 24-inch monitor with stunning picture quality." },
            new ProductDTO { Id = 5, Name = "Tablet", Price = 499.99m, Category = "Electronics", StockQuantity = 15, IsAvailable = true, ImageUrl = "http://example.com/tablet.jpg", CreatedDate = DateTime.Now.AddMonths(-5), UpdatedDate = DateTime.Now, Description = "A lightweight tablet, perfect for reading and browsing." },
            new ProductDTO { Id = 6, Name = "Gaming Chair", Price = 199.99m, Category = "Furniture", StockQuantity = 25, IsAvailable = true, ImageUrl = "http://example.com/gamingchair.jpg", CreatedDate = DateTime.Now.AddMonths(-4), UpdatedDate = DateTime.Now, Description = "Ergonomic gaming chair for extended comfort." },
            new ProductDTO { Id = 7, Name = "Keyboard", Price = 89.99m, Category = "Accessories", StockQuantity = 30, IsAvailable = true, ImageUrl = "http://example.com/keyboard.jpg", CreatedDate = DateTime.Now.AddMonths(-6), UpdatedDate = DateTime.Now, Description = "Mechanical keyboard with customizable RGB lighting." },
            new ProductDTO { Id = 8, Name = "Wireless Mouse", Price = 49.99m, Category = "Accessories", StockQuantity = 40, IsAvailable = true, ImageUrl = "http://example.com/mouse.jpg", CreatedDate = DateTime.Now.AddMonths(-2), UpdatedDate = DateTime.Now, Description = "A wireless mouse with high precision and long battery life." },
            new ProductDTO { Id = 9, Name = "Smartwatch", Price = 299.99m, Category = "Electronics", StockQuantity = 10, IsAvailable = true, ImageUrl = "http://example.com/smartwatch.jpg", CreatedDate = DateTime.Now.AddMonths(-7), UpdatedDate = DateTime.Now, Description = "A stylish smartwatch with health tracking features." },
            new ProductDTO { Id = 10, Name = "External Hard Drive", Price = 149.99m, Category = "Accessories", StockQuantity = 20, IsAvailable = true, ImageUrl = "http://example.com/harddrive.jpg", CreatedDate = DateTime.Now.AddMonths(-8), UpdatedDate = DateTime.Now, Description = "A 1TB external hard drive for backups and storage." }
        );
    }
}

