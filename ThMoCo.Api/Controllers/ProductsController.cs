using Microsoft.AspNetCore.Mvc;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ThMoCo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET /api/products
    [HttpGet]
    public ActionResult<List<ProductDTO>> GetProducts(
        [FromQuery] string? search,
        [FromQuery] string? category,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice)
    {
        var products = _productService.GetProducts(search, category, minPrice, maxPrice);
        return Ok(products);
    }

    // GET /api/products/{productId}
    [HttpGet("{productId}")]
    public ActionResult<ProductDTO> GetProductById(int productId)
    {
        var product = _productService.GetProductById(productId);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    // GET /api/products/stock
    [Authorize]
    [HttpGet("stock")]
    public async Task<ActionResult<List<ProductDTO>>> GetStockStatus()
    {
        var stockStatus = await _productService.GetStockStatus();
        return Ok(stockStatus);
    }

    // POST /api/products/update
    [HttpPost("update")]
    [Authorize(Roles = "Admin")] // Ensure this is accessible only to admin users
    public async Task<IActionResult> UpdateProductCatalog([FromBody] List<ProductDTO> updatedProducts)
    {
        await _productService.UpdateProductCatalog(updatedProducts);

        var response = new ApiResponse
        {
            Message = "Product catalog updated successfully."
        };
        return Ok(response);
    }
    // GET /api/products/categories
    [HttpGet("categories")]
    [Authorize]
    public ActionResult<List<string>> GetCategories()
    {
        var categories = _productService.GetProductCategories();
        return Ok(categories);
    }
}
