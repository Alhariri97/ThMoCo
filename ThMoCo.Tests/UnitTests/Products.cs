using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThMoCo.Api.Controllers;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;

namespace ThMoCo.Tests.UnitTests;
public class Products
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly ProductsController _controller;

    public Products()
    {
        _mockProductService = new Mock<IProductService>();
        _controller = new ProductsController(_mockProductService.Object);
    }

    [Fact]
    public void GetProducts_ReturnsOkResult_WithListOfProducts()
    {
        // Arrange
        var products = new List<ProductDTO>
        {
            new ProductDTO { Id = 1, Name = "Product1", Price = 10.0m },
            new ProductDTO { Id = 2, Name = "Product2", Price = 15.0m }
        };
        _mockProductService.Setup(service => service.GetProducts(null, null, null, null))
                           .Returns(products);

        // Act
        var result = _controller.GetProducts(null, null, null, null);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProducts = Assert.IsType<List<ProductDTO>>(okResult.Value);
        Assert.Equal(2, returnedProducts.Count);
    }

    [Fact]
    public void GetProductById_ProductExists_ReturnsOkResult()
    {
        // Arrange
        var product = new ProductDTO { Id = 1, Name = "Product1", Price = 10.0m };
        _mockProductService.Setup(service => service.GetProductById(1))
                           .Returns(product);

        // Act
        var result = _controller.GetProductById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProduct = Assert.IsType<ProductDTO>(okResult.Value);
        Assert.Equal(1, returnedProduct.Id);
    }

    [Fact]
    public void GetProductById_ProductDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        _mockProductService.Setup(service => service.GetProductById(1))
                           .Returns((ProductDTO)null);

        // Act
        var result = _controller.GetProductById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetStockStatus_ReturnsOkResult_WithStockData()
    {
        // Arrange
        var stockStatus = new List<ProductDTO>
        {
            new ProductDTO { Id = 1, Name = "Product1", IsAvailable = true },
            new ProductDTO { Id = 2, Name = "Product2", IsAvailable = false }
        };
        _mockProductService.Setup(service => service.GetStockStatus())
                           .ReturnsAsync(stockStatus);

        // Act
        var result = await _controller.GetStockStatus();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedStock = Assert.IsType<List<ProductDTO>>(okResult.Value);
        Assert.Equal(2, returnedStock.Count);
    }

    [Fact]
    public async Task UpdateProductCatalog_ValidProducts_ReturnsOkResult()
    {
        // Arrange
        var updatedProducts = new List<ProductDTO>
        {
                new ProductDTO { Name = "Laptop 99", Price = 999.99m, Category = "Electronics", StockQuantity = 10, IsAvailable = true, ImageUrl = "http://example.com/laptop.jpg", CreatedDate = DateTime.Now.AddMonths(-6), UpdatedDate = DateTime.Now, Description = "A high-performance laptop for work and gaming." },
        };

        _mockProductService.Setup(service => service.UpdateProductCatalog(updatedProducts))
                           .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateProductCatalog(updatedProducts);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result); // Ensure OkObjectResult is returned
        Assert.NotNull(okResult.Value); // Ensure Value is not null

        var apiResponse = Assert.IsType<ApiResponse>(okResult.Value); // Assert that the response is of type ApiResponse
        Assert.Equal("Product catalog updated successfully.", apiResponse.Message); // Check the Message property
    }

    [Fact]
    public void GetCategories_ReturnsOkResult_WithCategories()
    {
        // Arrange
        var categories = new List<string> { "Electronics", "Clothing" };
        _mockProductService.Setup(service => service.GetProductCategories())
                           .Returns(categories);

        // Act
        var result = _controller.GetCategories();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCategories = Assert.IsType<List<string>>(okResult.Value);
        Assert.Equal(2, returnedCategories.Count);
    }
}


