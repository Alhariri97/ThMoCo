
using Microsoft.EntityFrameworkCore;
using ThMoCo.Api.Data;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;

namespace ThMoCo.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IProfileService _profileService;

        public OrderService(AppDbContext context, IProfileService profileService)
        {
            _context = context;
            _profileService = profileService;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await Task.FromResult(_context.Orders.ToList());
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = _context.Orders.Include(o => o.Items).FirstOrDefault(o => o.Id == id);
            return order;

        }

        public async Task<Order> CreateOrderAsync(OrderCreateRequest orderRequest)
        {
            var existingUser = await _context.AppUsers.FindAsync(orderRequest.ProfileId);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            var paymentCard = _profileService.GetPaymentCard(existingUser.UserAuthId);
            var address = _profileService.GetAddress(existingUser.UserAuthId);
            if (address == null)
            {
                throw new Exception("No address found for the user.");
            }
            if (paymentCard == null)
            {
                throw new Exception("No Payment Card found for the user.");
            }

            decimal orderTotal = orderRequest.Items.Sum(i => i.Quantity * i.PricePerUnit);

            if (existingUser.Fund == null || existingUser.Fund < orderTotal)
            {
                throw new Exception("Insufficient funds to complete this purchase.");
            }

            // Validate product stock
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

            // Deduct stock
            foreach (var item in orderRequest.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                product.StockQuantity -= item.Quantity;
                _context.Products.Update(product);
            }

            // Deduct funds
            existingUser.Fund -= orderTotal;
            _context.AppUsers.Update(existingUser);

            // Create the order
            var order = new Order
            {
                ProfileId = orderRequest.ProfileId,
                CreatedAt = DateTime.UtcNow
            };

            // Attach OrderItems explicitly
            foreach (var item in orderRequest.Items)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    PricePerUnit = item.PricePerUnit
                };

                // Ensure EF Core tracks the new OrderItem
                _context.Entry(orderItem).State = EntityState.Added;

                order.Items.Add(orderItem);
            }

            // Calculate total
            order.CalculateTotalAmount();

            // Save the order
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving order: " + ex.Message, ex);
            }

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
            var orders = _context.Orders.Include(o => o.Items).Where(o => o.ProfileId == userId).ToList();
            return orders;
        }

    }

}