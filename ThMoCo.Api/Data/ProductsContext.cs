using Microsoft.EntityFrameworkCore;
using ThMoCo.Api.DTO;

namespace ThMoCo.Api.Data;

public class ProductsContext : DbContext
{
    public ProductsContext(DbContextOptions<ProductsContext> options)
        : base(options)
    {
    }

    public DbSet<ProductDTO> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductDTO>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
        });
    }
}
