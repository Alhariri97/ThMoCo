using ThMoCo.Api.DTO;

namespace ThMoCo.Api.IServices;

public interface IProductService
{
    // Retrieves a list of products based on optional filtering parameters
    List<ProductDTO> GetProducts(string? search, string? category, decimal? minPrice, decimal? maxPrice);

    // Retrieves a single product by its ID
    ProductDTO? GetProductById(int id);

    // Fetches stock status for all products
    Task<List<ProductDTO>> GetStockStatus();

    // Updates the product catalog and prices, typically from a supplier source (Admin-only access)
    Task UpdateProductCatalog(List<ProductDTO> updatedProducts);

    // Retrieves a list of distinct product categories
    List<string> GetProductCategories();
    Task<bool> UpdateProduct(ProductDTO updatedProduct);

}
