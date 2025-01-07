
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using ThMoCo.WebApp.DTO;
using ThMoCo.WebApp.IServices;

namespace ThMoCo.WebApp.Services;

public class AdminService : IAdminService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AdminService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<OrderDTO>> GetAllOrders()
    {
        CreateAuthenticatedClientAsync();
        return await _httpClient.GetFromJsonAsync<List<OrderDTO>>($"api/admin/orders");
    }

    public async Task<List<AppUserDTO>> GeAlltUsers()
    {
        CreateAuthenticatedClientAsync();
        return await _httpClient.GetFromJsonAsync<List<AppUserDTO>>("api/admin/customers");
    }

    public async Task<List<OrderDTO>> GetOrdersToDispatch()
    {
        CreateAuthenticatedClientAsync();
        return await _httpClient.GetFromJsonAsync<List<OrderDTO>>("api/admin/orders/dispatch");
    }

    public async Task MarkOrderAsDispatched(int orderId)
    {
        CreateAuthenticatedClientAsync();
        var response = await _httpClient.PutAsync($"api/admin/orders/{orderId}/dispatch", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task<AppUserDTO> GetCustomerProfile(int userId)
    {
        CreateAuthenticatedClientAsync();
        return await _httpClient.GetFromJsonAsync<AppUserDTO>($"api/admin/customers/{userId}");
    }

    public async Task DeleteCustomerAccount(int userId)
    {
        CreateAuthenticatedClientAsync();
        var response = await _httpClient.DeleteAsync($"api/admin/customers/{userId}");
        response.EnsureSuccessStatusCode();
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
