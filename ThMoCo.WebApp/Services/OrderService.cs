using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using ThMoCo.WebApp.IServices;
using ThMoCo.WebApp.Models;

namespace ThMoCo.WebApp.Services;


public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public OrderService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpClient = httpClient;
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
            var response = await httpClient.PutAsJsonAsync($"/api/order", orderRequest);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderDTO>();
        }
        catch (Exception ex)
        {
        }
        throw new NotImplementedException();
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
}
