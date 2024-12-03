namespace ThMoCo.Api.Data;

public class ProductService : IProductService
{
    private readonly ProductsContext _context;

    public ProductService(ProductsContext context)
    {
        _context = context;
    }

    public List<Product> GetProducts()
    {
        return _context.Products.ToList();
    }

    public Product? GetProductById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }
}
