
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using ThMoCo.Api.DTO;


namespace ThMoCo.Tests.Intergration;


public class ProductsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<ThMoCo.Api.Program>>
{
    private readonly HttpClient _clientWithoutAuth;
    public ProductsControllerIntegrationTests(CustomWebApplicationFactory<ThMoCo.Api.Program> factory)
    {
        _clientWithoutAuth = factory.CreateClient();

    }
    [Fact]
    public async Task GetProductById_ValidId_ReturnsProduct()
    {
        // Act
        var response = await _clientWithoutAuth.GetAsync("/api/products/1");

        // Assert
        response.EnsureSuccessStatusCode();
        var product = await response.Content.ReadFromJsonAsync<ProductDTO>();
        Assert.NotNull(product);
        Assert.Equal(1, product.Id);
    }

    [Fact]
    public async Task GetProductById_InvalidId_ReturnsNotFound()
    {
        // Act
        var response = await _clientWithoutAuth.GetAsync("/api/products/999");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetProducts_ReturnsProductsList()
    {
        // Act
        var response = await _clientWithoutAuth.GetAsync("/api/products");

        // Assert
        response.EnsureSuccessStatusCode();
        var products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
        Assert.NotNull(products);
        Assert.NotEmpty(products);
    }



    [Fact]
    public async Task GetCategories_Unauthorized_ReturnsUnauthorized()
    {

        // Act
        var response = await _clientWithoutAuth.GetAsync("/api/products/categories");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateProductCatalog_Unauthorized_ReturnsUnauthorized()
    {

        var newProducts = new List<ProductDTO>
        {
            new ProductDTO { Name = "Unauthorized Product", Price = 10.0m, Category = "Invalid", StockQuantity = 1 }
        };

        // Act
        var response = await _clientWithoutAuth.PostAsJsonAsync("/api/products/update", newProducts);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }
}

public class ProductsControllerIntegrationTestsWithUserRole : IClassFixture<CustomWebApplicationFactory<ThMoCo.Api.Program>>
{

    private readonly HttpClient _client;

    public ProductsControllerIntegrationTestsWithUserRole(CustomWebApplicationFactory<ThMoCo.Api.Program> factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "User"); // Matches TestAuthHandler logic

    }


    [Fact]
    public async Task GetCategories_ReturnsCategoryList()
    {
        // Act
        var response = await _client.GetAsync("/api/products/categories");

        // Assert
        response.EnsureSuccessStatusCode();
        var categories = await response.Content.ReadFromJsonAsync<List<string>>();
        Assert.NotNull(categories);
        Assert.NotEmpty(categories);
    }

    [Fact]
    public async Task UpdateProductCatalog_ValidData_ReturnsForbidden()
    {
        // Arrange
        var newProducts = new List<ProductDTO>
    {
        new ProductDTO { Name = "Test Product", Price = 99.99m, Category = "Test Category", StockQuantity = 10 }
    };

        // Act
        var response = await _client.PostAsJsonAsync("/api/products/update", newProducts);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
    }
}

public class ProductsControllerIntegrationTestsWithAdminRole : IClassFixture<CustomWebApplicationFactory<ThMoCo.Api.Program>>
{
    private readonly HttpClient _client;

    public ProductsControllerIntegrationTestsWithAdminRole(CustomWebApplicationFactory<ThMoCo.Api.Program> factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "admin-token");
    }


    [Fact]
    public async Task UpdateProductCatalog_ValidData_UpdatesCatalog()
    {
        // Arrange
        var newProducts = new List<ProductDTO>
        {
            new ProductDTO { Name = "Test Product", Price = 99.99m, Category = "Test Category", StockQuantity = 10 }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/products/update", newProducts);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        Assert.Equal("Product catalog updated successfully.", result.Message);
    }
}
