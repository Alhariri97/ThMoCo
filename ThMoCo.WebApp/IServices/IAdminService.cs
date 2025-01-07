using ThMoCo.WebApp.Models;

namespace ThMoCo.WebApp.IServices
{
    public interface IAdminService
    {
        Task<List<OrderDTO>> GetAllOrdersForUserAsync(int userId);
        Task<List<OrderDTO>> GetAllOrdersAsync(); 
        Task<List<AppUserDTO>> GetAllUsersAsync();
        Task<AppUserDTO> GetUserAsync();
        Task<OrderDTO> UpdateOrderStatusAsync(UpdateOrderStatusRequest orderRequest);

    }
}
