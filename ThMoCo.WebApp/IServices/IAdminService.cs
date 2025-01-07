using ThMoCo.WebApp.Models;

namespace ThMoCo.WebApp.IServices
{
    public interface IAdminService
    {
        Task<List<OrderDTO>> GetAllOrders();
        Task<List<AppUserDTO>> GeAlltUsers(); 
        Task<List<OrderDTO>> GetOrdersToDispatch();
        Task MarkOrderAsDispatched(int orderId);
        Task<AppUserDTO> GetCustomerProfile(int userId);
        Task DeleteCustomerAccount(int userId);
    }
}
