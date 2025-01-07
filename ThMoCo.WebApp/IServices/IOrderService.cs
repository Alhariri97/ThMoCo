using ThMoCo.WebApp.DTO;
using ThMoCo.WebApp.Models;

namespace ThMoCo.WebApp.IServices;

public interface IOrderService
{
    Task<List<OrderDTO>> GetAllOrdersAsync();
    Task<List<OrderDTO>> GetAllOrdersForUserAsync(int userId); 
    Task<OrderDTO> GetOrderByIdAsync(int id);
    Task<OrderDTO> CreateOrderAsync(OrderCreateRequest orderRequest);
    Task<OrderDTO> UpdateOrderAsync(int id, OrderUpdateRequest orderRequest);
    Task<bool> DeleteOrderAsync(int id);
}
