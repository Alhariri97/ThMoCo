
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
            var existingUser = await _context.AppUsers.FindAsync(orderRequest.ProfileId);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            if (existingUser.AddressId == null)
            {
                throw new Exception("No address found for the user.");
            }

            decimal orderTotal = orderRequest.Items.Sum(i => i.Quantity * i.PricePerUnit);

            if (existingUser.Fund == null || existingUser.Fund < orderTotal)
            {
                throw new Exception("Insufficient funds to complete this purchase.");
            }

            foreach (var item in orderRequest.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {item.ProductId} does not exist.");
                }
                if (product.StockQuantity < item.Quantity)
                {
                    throw new Exception($"Not enough stock for product '{product.Name}'. Available: {product.StockQuantity}, Requested: {item.Quantity}.");
                }
            }

            foreach (var item in orderRequest.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                product.StockQuantity -= item.Quantity;
                _context.Products.Update(product);
            }

            existingUser.Fund -= orderTotal;
            _context.AppUsers.Update(existingUser);

            var order = new Order
            {
                ProfileId = orderRequest.ProfileId,
                CreatedAt = DateTime.UtcNow,
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
            var orders = _context.Orders.Where(o => o.ProfileId == userId).ToList();
            return orders;
        }

    }

}