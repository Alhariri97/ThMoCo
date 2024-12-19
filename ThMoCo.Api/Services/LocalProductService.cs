using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;

namespace ThMoCo.Api.Services;


public class LocalProductService : IProductService
{
    private readonly List<ProductDTO> _products = new List<ProductDTO>
    {
        new ProductDTO { Id = 1, Name = "Laptop", Price = 999.99m },
        new ProductDTO { Id = 2, Name = "Smartphone", Price = 799.99m },
        new ProductDTO { Id = 3, Name = "Headphones", Price = 199.99m },
        new ProductDTO { Id = 4, Name = "Monitor", Price = 299.99m }
    };

    public List<ProductDTO> GetProducts()
    {
        return _products;
    }

    public ProductDTO? GetProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }
}
