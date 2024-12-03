using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ThMoCo.Api.Data;

/// <summary>
/// create a DesignTimeDbContextFactory which EF Core will use to create your DbContext during design-time
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProductsContext>
{
    public ProductsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductsContext>();

        // Get the connection string from appsettings.json or other configuration sources
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("ConnectionString");

        optionsBuilder.UseSqlServer(connectionString);

        return new ProductsContext(optionsBuilder.Options);
    }
}
