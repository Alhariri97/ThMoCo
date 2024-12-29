using ThMoCo.WebApp.Models;

namespace ThMoCo.WebApp.IServices
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProductsAsync(string? search = null, string? category = null, decimal? minPrice = null, decimal? maxPrice = null);
        Task<ProductDTO> GetProductByIdAsync(int productId);
        Task<List<ProductDTO>> GetStockStatusAsync();
        Task UpdateProductCatalogAsync(List<ProductDTO> updatedProducts);
        Task<List<string>> GetCategoriesAsync();
    }
}
