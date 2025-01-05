using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ThMoCo.Api.DTO;

namespace ThMoCo.Tests.IntegrationTests;

public class Orders : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client;
    public Orders(CustomWebApplicationFactory<Api.Program> factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "valid-user-token"); 

    }
    [Fact]
    public async Task GetAllOrders_Unauthorized_ReturnsUnauthorized()
    {
        // Act
        var response = await _client.GetAsync("/api/order");

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_ReturnsBadRequest_WhenDataInvalid()
    {
    // Arrange
    var orderRequest = new OrderCreateRequest
        {
            ProfileId = 0, // Invalid profile
            Items = new List<OrderItemDTO> { new OrderItemDTO { ProductName = "Laptob", ProductId = 1, Quantity = 1, PricePerUnit = 20 } }
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/order", jsonContent);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetOrderById_Unauthorized_ReturnsUnauthorized()
    {
        // Act
        var response = await _client.GetAsync("/api/order/1");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteOrder_Unauthorized_ReturnsUnauthorized()
    {
        // Act
        var response = await _client.DeleteAsync("/api/order/1");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

}
