
using ThMoCo.Api.IServices;
using ThMoCo.Api.Models;

namespace ThMoCo.Api.Services;

public class LocalAdminService : IAdminService
{
    private readonly List<Order> _orders = new List<Order>();
    private readonly List<AppUser> _users = new List<AppUser>();

    public LocalAdminService()
    {
        // Sample orders for testing
        _orders = new List<Order>
        {
            new Order { Id = 1, ProfileId = 1, IsDispatched = false, DispatchDate = null },
            new Order { Id = 2, ProfileId = 2, IsDispatched = true, DispatchDate = DateTime.UtcNow.AddDays(-2) }
        };

        // Sample users for testing
        _users = new List<AppUser>
        {
            new AppUser { Id = 1, Name = "John Doe", Email = "john.doe@example.com", UserAuthId = "auth0|12345", Fund = 100.50m, IsEmailVerified = true },
            new AppUser { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", UserAuthId = "auth0|67890", Fund = 250.75m, IsEmailVerified = true }
        };
    }

    public async Task<IEnumerable<Order>> GetOrdersToDispatchAsync()
    {
        return await Task.FromResult(_orders.Where(o => !o.IsDispatched).ToList());
    }

    public async Task<bool> MarkOrderAsDispatchedAsync(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null) return false;

        order.UpdatedAt = DateTime.UtcNow;
        order.IsDispatched = true;
        order.DispatchDate = DateTime.UtcNow;

        return await Task.FromResult(true);
    }

    public async Task<AppUser> GetCustomerProfileAsync(int customerId)
    {
        return await Task.FromResult(_users.FirstOrDefault(u => u.Id == customerId));
    }

    public async Task<bool> DeleteCustomerAccountAsync(int customerId)
    {
        var user = _users.FirstOrDefault(u => u.Id == customerId);
        if (user == null) return false;

        user.Name = "Anonymous";
        user.Email = "anonymous@example.com";
        user.UserAuthId = null;
        user.Fund = 0;
        user.IsEmailVerified = false;

        return await Task.FromResult(true);
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await Task.FromResult(_orders.ToList());
    }

    public async Task<List<AppUser>> GetCustomers()
    {
        return await Task.FromResult(_users.ToList());
    }
}
