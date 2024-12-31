using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ThMoCo.WebApp.Models;
using System.Collections.Generic;
using ThMoCo.WebApp.IServices;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace ThMoCo.WebApp.Controllers;

public class AdminController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IProductService _productService;

    public AdminController(IHttpClientFactory httpClientFactory,
        IProductService productService)
    {
        _httpClientFactory = httpClientFactory;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient();

        var response = await client.GetAsync("http://undercutters.azurewebsites.net/api/product");
        if (!response.IsSuccessStatusCode)
        {
            return View("Error");
        }

        var productsJson = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<SupplierProduct>>(productsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var availableProducts = products.Where(p => p.InStock ).ToList();

        return View(availableProducts);
    }

    [HttpPost]
    public async Task<IActionResult> OrderProduct(int productId , int quantity)
    {
        var client = _httpClientFactory.CreateClient();
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


        // Fetch product details from the API
        var productResponse = await client.GetAsync($"http://undercutters.azurewebsites.net/api/product/{productId}");
        if (!productResponse.IsSuccessStatusCode)
        {
            return View("Error"); // Handle case where product is not found
        }

        // Deserialize product details
        var productJson = await productResponse.Content.ReadAsStringAsync();

        var product = JsonSerializer.Deserialize<SupplierProduct>(productJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (product == null || !product.InStock)
        {
            return View("Error", "Product is not available or in stock."); // Optional error message
        }
        //todo : change the hardcoded values to be dynamic from the user's logged in and let the user able to choose quanitity.
        // Prepare the order payload
        var orderPayload = new { ProductId = productId, AccountName = "sample string 1", CardNumber = "sample string 2", Quantity= quantity };
        var content = new StringContent(JsonSerializer.Serialize(orderPayload), System.Text.Encoding.UTF8, "application/json");

        // Send the order request
      
        var orderResponse = await client.PostAsync("http://undercutters.azurewebsites.net/api/order", content);
        var orderJson = await productResponse.Content.ReadAsStringAsync();
        var order = JsonSerializer.Deserialize<SupplierProduct>(orderJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (!orderResponse.IsSuccessStatusCode)
        {
            return View("Error");
        }

        ProductDTO newlyBoughtProduct = ConvertToProductDTO(product, quantity);
        var productDTOList = new List<ProductDTO> { newlyBoughtProduct };

        //var accessToken = await HttpContext.GetTokenAsync("access_token");

        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        _productService.UpdateProductCatalogAsync(productDTOList); 
        // Redirect back to Index after successfully placing an order
        return RedirectToAction(nameof(Index));
    }


    private ProductDTO ConvertToProductDTO(SupplierProduct supplierProduct, int quantity)
    {
        return new ProductDTO
        {
            Name = supplierProduct.Name,
            Description = supplierProduct.Description,
            Price = supplierProduct.Price * 1.1m, // Apply price adjustment if required
            Category = supplierProduct.CategoryName,
            StockQuantity = quantity // Use the ordered quantity as the new stock
        };
    }
}
