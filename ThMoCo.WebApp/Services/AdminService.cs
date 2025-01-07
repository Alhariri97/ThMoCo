using ThMoCo.WebApp.IServices;
using ThMoCo.WebApp.Models;

namespace ThMoCo.WebApp.Services
{
    public class AdminService : IAdminService
    {
        public Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDTO>> GetAllOrdersForUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppUserDTO>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AppUserDTO> GetUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDTO> UpdateOrderStatusAsync(UpdateOrderStatusRequest orderRequest)
        {
            throw new NotImplementedException();
        }
    }
}
