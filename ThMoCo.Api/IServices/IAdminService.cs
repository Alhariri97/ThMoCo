using ThMoCo.Api.DTO;

namespace ThMoCo.Api.IServices
{
    public interface IAdminService
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetOrdersToDispatchAsync();
        Task<bool> MarkOrderAsDispatchedAsync(int orderId);

        Task<List<AppUser>> GetCustomers();
        Task<AppUser> GetCustomerProfileAsync(int customerId);
        Task<bool> DeleteCustomerAccountAsync(int customerId);

    }
}
