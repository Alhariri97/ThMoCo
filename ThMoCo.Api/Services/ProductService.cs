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
        return _context.Products.ToList();
    }

    public ProductDTO? GetProductById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }
}
