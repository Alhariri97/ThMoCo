
using ThMoCo.WebApp.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using ThMoCo.WebApp.Models;



public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IProductService _productService;
    private readonly string _baseAddress;
    private readonly IProfileService _profileService;

    public HomeController(IHttpClientFactory httpClientFactory,
        IProductService productService, IConfiguration configuration,
        IProfileService profileService)
    {
        _httpClientFactory = httpClientFactory;
        _productService = productService;
        _baseAddress = configuration["Values:BaseAddress"];
        _profileService = profileService;

    }

    public async Task<IActionResult> Index(string search, string category, decimal? minPrice, decimal? maxPrice)
    {
        var products = await _productService.GetProductsAsync(search, category, minPrice, maxPrice);
        var categories = await _productService.GetCategoriesAsync();
        ViewBag.Categories = categories;

        
        HomeViewModel homeViewModel = new HomeViewModel()
        {
            Products = products,
            Categories = categories,
        };

        var currentPhotoClaim = User.Claims.FirstOrDefault(c => c.Type == "PhotoUrl");

        return View(homeViewModel);
    }
    public async Task<IActionResult> Products(string? search, string? category, decimal? minPrice, decimal? maxPrice)
    {
        var products = await _productService.GetProductsAsync(search, category, minPrice, maxPrice);
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }




    [HttpGet]
    public async Task<IActionResult> CallProtectedApi()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        if (string.IsNullOrEmpty(accessToken))
        {
            return Content("No access token found. Make sure you're logged in and the scope/audience is set properly.");
        }

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await client.GetAsync($"{_baseAddress}protected");

        if (!response.IsSuccessStatusCode)
        {
            return Content($"Failed to call API. Status code: {response.StatusCode}");
        }

        var result = await response.Content.ReadAsStringAsync();
        return Content($"Protected API returned: {result}");
    }

    [HttpGet]
    public async Task<IActionResult> CallProtectedAdminApi()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        if (string.IsNullOrEmpty(accessToken))
        {
            return Content("No access token found. Make sure you're logged in and the scope/audience is set properly.");
        }

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await client.GetAsync($"{_baseAddress}api/Admin/secret");

        if (!response.IsSuccessStatusCode)
        {
            return Content($"Failed to call API. Status code: {response.StatusCode}");
        }

        var result = await response.Content.ReadAsStringAsync();
        return Content($"Protected API returned from admin: {result}");
    }
}
