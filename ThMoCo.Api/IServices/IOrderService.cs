using ThMoCo.Api.Models;

namespace ThMoCo.Api.IServices;


public interface IOrderService
{
    Task<List<Order>> GetAllOrdersAsync();
    //Task<List<Order>> GetAllOrdersForUserAsync(int userId);
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> CreateOrderAsync(OrderCreateRequest orderRequest);
    Task<Order> UpdateOrderAsync(int id, Order orderRequest); 
    Task<bool> DeleteOrderAsync(int id);
    // ✅ Ensure this method signature is present and matches exactly
    Task<List<Order>> GetAllOrdersForUserAsync(int userId);

}