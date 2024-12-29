using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThMoCo.WebApp.Models;
using ThMoCo.WebApp.IServices;

namespace ThMoCo.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index(string search, string category, decimal? minPrice, decimal? maxPrice)
        {
            var products = await _productService.GetProductsAsync(search, category, minPrice, maxPrice);
            var categories = await _productService.GetCategoriesAsync();
            ViewBag.Categories = categories;

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}



//using Microsoft.AspNetCore.Mvc;
//using ThMoCo.WebApp.IServices;
//namespace ThMoCo.WebApp.Controllers;

//public class HomeController : Controller
//{
//    private readonly IProductService _productService;

//    public HomeController(IProductService productService)
//    {
//        _productService = productService;
//    }

//    public async Task<IActionResult> Products(string? search, string? category, decimal? minPrice, decimal? maxPrice)
//    {
//        var products = await _productService.GetProductsAsync(search, category, minPrice, maxPrice);
//        return View(products);
//    }

//    public async Task<IActionResult> ProductDetails(int id)
//    {
//        var product = await _productService.GetProductByIdAsync(id);
//        return View(product);
//    }

//    public async Task<IActionResult> StockStatus()
//    {
//        var stockStatus = await _productService.GetStockStatusAsync();
//        return View(stockStatus);
//    }

//    public async Task<IActionResult> Categories()
//    {
//        var categories = await _productService.GetCategoriesAsync();
//        return View(categories);
//    }
//}
