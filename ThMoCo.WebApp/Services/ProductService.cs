using ThMoCo.WebApp.IServices;
using ThMoCo.WebApp.Models;

namespace ThMoCo.WebApp.Services;


public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductDTO>> GetProductsAsync(string? search = null, string? category = null, decimal? minPrice = null, decimal? maxPrice = null)
    {
        var queryParams = new List<string>();
        if (!string.IsNullOrEmpty(search)) queryParams.Add($"search={search}");
        if (!string.IsNullOrEmpty(category)) queryParams.Add($"category={category}");
        if (minPrice.HasValue) queryParams.Add($"minPrice={minPrice.Value}");
        if (maxPrice.HasValue) queryParams.Add($"maxPrice={maxPrice.Value}");

        var queryString = string.Join("&", queryParams);
        var response = await _httpClient.GetAsync($"/api/products?{queryString}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
    }

    public async Task<ProductDTO> GetProductByIdAsync(int productId)
    {
        var response = await _httpClient.GetAsync($"/api/products/{productId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDTO>();
    }

    public async Task<List<ProductDTO>> GetStockStatusAsync()
    {
        var response = await _httpClient.GetAsync("/api/products/stock");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
    }

    public async Task UpdateProductCatalogAsync(List<ProductDTO> updatedProducts)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/products/update", updatedProducts);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<string>> GetCategoriesAsync()
    {
        var response = await _httpClient.GetAsync("/api/products/categories");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<string>>();
    }
}
