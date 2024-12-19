using System.Diagnostics;
using ThMoCo.Api.Data;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;

namespace ThMoCo.Api.Services;

public class ProductService : IProductService
{
    private readonly ProductsContext _context;

    public ProductService(ProductsContext context)
    {
        _context = context;
    }

    public List<ProductDTO> GetProducts()
    {
        try
        {
            var list = _context.Products.ToList();
            return list;
        }
        catch (Exception ex)
        {
            // Log the exception
            Debug.WriteLine($"Error fetching products: {ex.Message}");
            throw new Exception($"Error getting data from the database. {ex.Message}");
        }
    }

    public ProductDTO? GetProductById(int id)
    {
        try
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }
        catch (Exception ex)
        {
            // Log the exception
            Debug.WriteLine($"Error fetching product with ID {id}: {ex.Message}");
            throw new Exception("Error getting data from the database.");
        }
    }
}
