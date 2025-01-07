using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Net.Http.Headers;
using ThMoCo.WebApp.DTO;
using ThMoCo.WebApp.IServices;
using ThMoCo.WebApp.Models;

namespace ThMoCo.WebApp.Services;


public class OrderService : IOrderService
{
    private readonly ISession _session;
    private const string CartSessionKey = "Cart";
    /// <summary>
    /// 
    /// </summary>
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderService(HttpClient httpClient,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpClient = httpClient;
        _session = _httpContextAccessor.HttpContext.Session;

    }

    public async Task<List<OrderDTO>> GetAllOrdersAsync()
    {
        try
        {
            var httpClient = await CreateAuthenticatedClientAsync();
            var response = await httpClient.GetAsync($"/api/order");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<OrderDTO>>();
        }
        catch (Exception ex)
        {
        }
        throw new NotImplementedException();
    }
    public async Task<OrderDTO> GetOrderByIdAsync(int id)
    {
        try
        {
            var httpClient = await CreateAuthenticatedClientAsync();
            var response = await httpClient.GetAsync($"/api/order/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderDTO>();
        }
        catch (Exception ex)
        {
        }
        throw new NotImplementedException();
    }
    public async Task<OrderDTO> CreateOrderAsync(OrderCreateRequest orderRequest)
    {
        try
        {
            var httpClient = await CreateAuthenticatedClientAsync();

            // Convert orderRequest to JSON content
            var jsonContent = JsonContent.Create(orderRequest);

            // Send HTTP POST request with JSON payload
            var response = await httpClient.PostAsync("/api/order", jsonContent);

            // Ensure the response is successful
            response.EnsureSuccessStatusCode();

            _session.Remove(CartSessionKey);
            // Deserialize the response content to OrderDTO
            return await response.Content.ReadFromJsonAsync<OrderDTO>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP Request failed: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }

    public async Task<OrderDTO> UpdateOrderAsync(int id, OrderUpdateRequest orderRequest)
    {
        try
        {
            var httpClient = await CreateAuthenticatedClientAsync();
            var response = await httpClient.PutAsJsonAsync($"/api/order/{id}", orderRequest);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderDTO>();
        }
        catch (Exception ex)
        {
        }
        throw new NotImplementedException();
    }
    public async Task<bool> DeleteOrderAsync(int id)
    {
        try
        {
            var httpClient = await CreateAuthenticatedClientAsync();
            var response = await httpClient.DeleteAsync($"/api/order/{id}");
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
        }
        throw new NotImplementedException();
    }

    public async Task<List<OrderDTO>> GetAllOrdersForUserAsync(int userId)
    {
        var httpClient = await CreateAuthenticatedClientAsync();
        var response = await httpClient.GetAsync($"/api/Order/user/{userId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<OrderDTO>>();

        throw new NotImplementedException();
    }


    private async Task<HttpClient> CreateAuthenticatedClientAsync()
    {
        // Retrieve the access token from the current HTTP context
        var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        if (!string.IsNullOrEmpty(accessToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
        return _httpClient;
    }
}
