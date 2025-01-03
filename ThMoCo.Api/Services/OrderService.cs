
using ThMoCo.Api.Data;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;

namespace ThMoCo.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await Task.FromResult(_context.Orders.ToList());
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await Task.FromResult(_context.Orders.FirstOrDefault(o => o.Id == id));
        }

        public async Task<Order> CreateOrderAsync(OrderCreateRequest orderRequest)
        {
            var order = new Order
            {
                ProfileId = orderRequest.ProfileId,
                Items = orderRequest.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    PricePerUnit = i.PricePerUnit
                }).ToList()
            };

            order.CalculateTotalAmount();

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> UpdateOrderAsync(int id, OrderUpdateRequest orderRequest)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return null;
            }

            order.Items = orderRequest.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                PricePerUnit = i.PricePerUnit
            }).ToList();

            order.CalculateTotalAmount();

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Order>> GetAllOrdersForUserAsync(int userId)
        {
            return await Task.FromResult(_context.Orders.Where(o => o.ProfileId == userId).ToList());
        }
    }

}