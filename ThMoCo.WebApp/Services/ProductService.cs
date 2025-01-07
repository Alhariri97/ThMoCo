using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using System.Net.Http.Headers;
using ThMoCo.WebApp.DTO;
using ThMoCo.WebApp.IServices;

namespace ThMoCo.WebApp.Services;


public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductService( HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpClient = httpClient;

    }
    private async Task<HttpClient> CreateAuthenticatedClientAsync()
    {
        // Retrieve the access token from the current HTTP context
        var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

        if (!string.IsNullOrEmpty(accessToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return _httpClient;
    }
    public async Task<List<ProductDTO>> GetProductsAsync(string? search = null, string? category = null, decimal? minPrice = null, decimal? maxPrice = null)
    {
        var httpClient = await CreateAuthenticatedClientAsync();
        var queryParams = new List<string>();
        if (!string.IsNullOrEmpty(search)) queryParams.Add($"search={search}");
        if (!string.IsNullOrEmpty(category)) queryParams.Add($"category={category}");
        if (minPrice.HasValue) queryParams.Add($"minPrice={minPrice.Value}");
        if (maxPrice.HasValue) queryParams.Add($"maxPrice={maxPrice.Value}");

        var queryString = string.Join("&", queryParams);
        var response = await httpClient.GetAsync($"/api/products?{queryString}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
    }

    public async Task<ProductDTO> GetProductByIdAsync(int productId)
    {
        var httpClient = await CreateAuthenticatedClientAsync();
        var response = await httpClient.GetAsync($"/api/products/{productId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDTO>();
    }

    public async Task<List<ProductDTO>> GetStockStatusAsync()
    {
        var httpClient = await CreateAuthenticatedClientAsync();

        var response = await httpClient.GetAsync("/api/products/stock");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
    }

    public async Task UpdateProductCatalogAsync(List<ProductDTO> updatedProducts)
    {
        var httpClient = await CreateAuthenticatedClientAsync();

        var response = await httpClient.PostAsJsonAsync("/api/products/update", updatedProducts);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<string>> GetCategoriesAsync()
    {
        var httpClient = await CreateAuthenticatedClientAsync();

        var response = await httpClient.GetAsync("/api/products/categories");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<string>>();
    }
}
