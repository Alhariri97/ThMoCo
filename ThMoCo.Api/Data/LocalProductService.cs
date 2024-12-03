namespace ThMoCo.Api.Data;


public class LocalProductService : IProductService
{
    public List<Product> GetProducts()
    {
        return new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 999.99m },
                new Product { Id = 2, Name = "Smartphone", Price = 799.99m },
                new Product { Id = 3, Name = "Headphones", Price = 199.99m },
                new Product { Id = 4, Name = "Monitor", Price = 299.99m }
            };
    }

    public Product? GetProductById(int id)
    {
        return GetProducts().FirstOrDefault(p => p.Id == id);
    }
}