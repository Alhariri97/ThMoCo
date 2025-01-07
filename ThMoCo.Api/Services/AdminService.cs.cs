using ThMoCo.Api.IServices;
using ThMoCo.Api.Models;

namespace ThMoCo.Api.Services;

public class AdminService : IAdminService
{
    private readonly IOrderService _orderService;
    private readonly IProfileService _profileService;
    public AdminService(IOrderService orderService, IProfileService profileService)
    {
        _orderService = orderService;
        _profileService = profileService;
    }

    public async Task<IEnumerable<Order>> GetOrdersToDispatchAsync()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return orders.Where(o => !o.IsDispatched).ToList();
    }

    public async Task<bool> MarkOrderAsDispatchedAsync(int orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        if (order == null) return false;

        order.UpdatedAt = DateTime.UtcNow;
        order.IsDispatched = true;
        order.DispatchDate = DateTime.UtcNow;
        await _orderService.UpdateOrderAsync(orderId, order);
        return true;
    }

    public async Task<AppUser> GetCustomerProfileAsync(int customerId)
    {
        return  _profileService.GetUserByIdAsync(customerId);
    }

    public async Task<bool> DeleteCustomerAccountAsync(int customerId)
    {
        var customer =  _profileService.GetUserByIdAsync(customerId);
        if (customer == null) return false;

        var deleteUser = _profileService.AnonymiseCustomerDataAsync(customerId);
        return deleteUser;
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return orders;
    }

    public  async Task<List<AppUser>> GetCustomers()
    {
        var customer =  _profileService.GetAllUsers();
        return customer;
    }
}
