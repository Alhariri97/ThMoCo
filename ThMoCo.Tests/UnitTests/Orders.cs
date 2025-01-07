using Microsoft.AspNetCore.Mvc;
using Moq;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;
using ThMoCo.Api.Controllers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ThMoCo.Api.Models;

namespace ThMoCo.Tests.UnitTests;

public class Orders
{
    private readonly Mock<IOrderService> _mockOrderService;
    private readonly Mock<IProfileService> _mockProfileService;
    private readonly Mock<IProductService> _mockProductService;
    private readonly OrderController _controller;

    public Orders()
    {
        _mockOrderService = new Mock<IOrderService>();
        _mockProfileService = new Mock<IProfileService>();
        _mockProductService = new Mock<IProductService>();

        _controller = new OrderController(
            _mockOrderService.Object,
            _mockProfileService.Object,
            _mockProductService.Object
        );

        // **Mocking User Claims**
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public async Task GetAllOrders_ReturnsOk_WithOrders()
    {
        // Arrange
        var orders = new List<Order> { new Order { Id = 1, ProfileId = 2 } };
        _mockOrderService.Setup(s => s.GetAllOrdersAsync()).ReturnsAsync(orders);

        // Act
        var result = await _controller.GetAllOrders();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedOrders = Assert.IsType<List<Order>>(okResult.Value);
        Assert.Single(returnedOrders);
    }

    [Fact]
    public async Task GetOrderById_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        // Arrange
        _mockProfileService.Setup(s => s.GetUserByAuthIdAsync(It.IsAny<string>())).Returns(new AppUser { Id = 2 });

        // Act
        var result = await _controller.GetOrderById(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateOrder_ReturnsCreatedAtAction_WhenValid()
    {
        // Arrange
        var orderRequest = new OrderCreateRequest { ProfileId = 2, Items = new List<OrderItemDTO>() };
        var createdOrder = new Order { Id = 1, ProfileId = 2 };

        _mockProfileService.Setup(s => s.GetUserByAuthIdAsync(It.IsAny<string>())).Returns(new AppUser { Id = 2 });
        _mockOrderService.Setup(s => s.CreateOrderAsync(It.IsAny<OrderCreateRequest>())).ReturnsAsync(createdOrder);

        // Act
        var result = await _controller.CreateOrder(orderRequest);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedOrder = Assert.IsType<Order>(createdResult.Value);
        Assert.Equal(1, returnedOrder.Id);
    }

    [Fact]
    public async Task DeleteOrder_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
    _mockProfileService
        .Setup(s => s.GetUserByAuthIdAsync(It.IsAny<string>()))
        .Returns(new AppUser { Id = 1, UserAuthId = "user123" }); // Mock return value

    _mockOrderService
        .Setup(s => s.GetOrderByIdAsync(It.IsAny<int>()))
        .ReturnsAsync(new Order { Id = 1, ProfileId = 1 }); // Mock order retrieval

    _mockOrderService
        .Setup(s => s.DeleteOrderAsync(It.IsAny<int>()))
        .ReturnsAsync(true);
            // Act
            var result = await _controller.DeleteOrder(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

}
