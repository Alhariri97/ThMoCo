using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ThMoCo.WebApp.Models;
using System.Collections.Generic;
using ThMoCo.WebApp.IServices;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using ThMoCo.WebApp.DTO;

namespace ThMoCo.WebApp.Controllers;

public class AdminController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IProductService _productService;
    private readonly IAdminService _adminService;

    public AdminController(IHttpClientFactory httpClientFactory,
        IProductService productService,
        IAdminService adminService)
    {
        _httpClientFactory = httpClientFactory;
        _productService = productService;
        _adminService = adminService;
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
        var availableProducts = products.Where(p => p.InStock).ToList();

        return View(availableProducts);
    }

    [HttpPost]
    public async Task<IActionResult> OrderProduct(int productId, int quantity)
    {
        var client = _httpClientFactory.CreateClient();
        var productResponse = await client.GetAsync($"http://undercutters.azurewebsites.net/api/product/{productId}");
        if (!productResponse.IsSuccessStatusCode)
        {
            return View("Error");
        }

        var productJson = await productResponse.Content.ReadAsStringAsync();
        var product = JsonSerializer.Deserialize<SupplierProduct>(productJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (product == null || !product.InStock)
        {
            return View("Error", "Product is not available or in stock.");
        }

        var orderPayload = new { ProductId = productId, AccountName = "sample string 1", CardNumber = "sample string 2", Quantity = quantity };
        var content = new StringContent(JsonSerializer.Serialize(orderPayload), System.Text.Encoding.UTF8, "application/json");

        var orderResponse = await client.PostAsync("http://undercutters.azurewebsites.net/api/order", content);
        if (!orderResponse.IsSuccessStatusCode)
        {
            return View("Error");
        }

        var orderJson = await orderResponse.Content.ReadAsStringAsync();
        var order = JsonSerializer.Deserialize<SupplierProduct>(orderJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ProductDTO newlyBoughtProduct = ConvertToProductDTO(product, quantity);
        var productDTOList = new List<ProductDTO> { newlyBoughtProduct };

        await _productService.UpdateProductCatalogAsync(productDTOList);
        return RedirectToAction(nameof(Index));
    }

    private ProductDTO ConvertToProductDTO(SupplierProduct supplierProduct, int quantity)
    {
        return new ProductDTO
        {
            Name = supplierProduct.Name,
            Description = supplierProduct.Description,
            Price = supplierProduct.Price * 1.1m,
            Category = supplierProduct.CategoryName,
            StockQuantity = quantity
        };
    }

    public async Task<IActionResult> Orders()
    {
        var orders = await _adminService.GetAllOrders();
        return View(orders);
    }

    //public async Task<IActionResult> OrderDetails(int id)
    //{
    //    var orders = await _adminService.GetAllOrders(id);
    //    return View(orders);
    //}

    public async Task<IActionResult> Profiles()
    {
        var profiles = await _adminService.GeAlltUsers();
        return View(profiles);
    }

    [HttpPost("Admin/UpdateOrderStatusAsync")]
    public async Task<IActionResult> UpdateOrderStatusAsync(int orderId)
    {
        await _adminService.MarkOrderAsDispatched(orderId);
        return RedirectToAction(nameof(Orders));
    }
}
