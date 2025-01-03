

using ThMoCo.Api.DTO;

namespace ThMoCo.Api.IServices;


public interface IOrderService
{
    Task<List<Order>> GetAllOrdersAsync();
    Task<List<Order>> GetAllOrdersForUserAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> CreateOrderAsync(OrderCreateRequest orderRequest);
    Task<Order> UpdateOrderAsync(int id, OrderUpdateRequest orderRequest);
    Task<bool> DeleteOrderAsync(int id);
}