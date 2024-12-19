using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ThMoCo.Api.IServices;

namespace ThMoCo.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IConfiguration _configuration;

    public ProductsController(IProductService productService, IConfiguration configuration)
    {
        _productService = productService;
        _configuration = configuration;

    }

    [HttpGet("connection-string")]
    public IActionResult GetAny()
    {
        string connectionString = _configuration.GetValue<string>("ConnectionStrings:ConnectionString");
        return Ok(new { ConnectionString = connectionString });
    }


    [HttpGet]
    public IActionResult Get()
    {
        var products = _productService.GetProducts();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
}
