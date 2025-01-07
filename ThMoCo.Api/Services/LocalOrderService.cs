
using Microsoft.EntityFrameworkCore;
using ThMoCo.Api.IServices;
using ThMoCo.Api.Models;

namespace ThMoCo.Api.Services
{
    public class LocalOrderService : IOrderService
    {
        private readonly List<Order> _localOrders;
        private int _nextOrderId;
        private readonly IProfileService _profileService;
        private readonly IProductService _productService;
        public LocalOrderService(IProfileService profileService,
            IProductService productService)
        {
            _nextOrderId = 3; // Start with 3 as two orders are pre-populated
            _localOrders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    ProfileId = 2,
                    CreatedAt = DateTime.UtcNow,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductId = 1, ProductName = "Keyboard", Quantity = 2, PricePerUnit = 29.99m },
                        new OrderItem { ProductId = 2, ProductName = "Mouse", Quantity = 1, PricePerUnit = 19.99m }
                    }
                },
                new Order
                {
                    Id = 2,
                    ProfileId = 102,
                    CreatedAt = DateTime.UtcNow,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductId = 3, ProductName = "Monitor", Quantity = 1, PricePerUnit = 199.99m },
                        new OrderItem { ProductId = 4, ProductName = "Desk Lamp", Quantity = 2, PricePerUnit = 15.99m }
                    }
                }
            };

            foreach (var order in _localOrders)
            {
                order.CalculateTotalAmount();
            }

            _profileService = profileService;
            _productService = productService;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await Task.FromResult(_localOrders);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = _localOrders.FirstOrDefault(o => o.Id == id);
            return await Task.FromResult(order);
        }

        public async Task<Order> CreateOrderAsync(OrderCreateRequest orderRequest)
        {
            // Fetch the user (Simulating the profile service)
            var existingUser =  _profileService.GetUserByIdAsync(orderRequest.ProfileId);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            // Calculate total order amount
            decimal orderTotal = orderRequest.Items.Sum(i => i.Quantity * i.PricePerUnit);

            // Check if the user has sufficient funds
            if (existingUser.Fund == null || existingUser.Fund < orderTotal)
            {
                throw new Exception("Insufficient funds to complete this purchase.");
            }
            // Check if the user has sufficient funds
            if (existingUser.Address == null)
            {
                throw new Exception("No address found for the user.");
            }
            if (string.IsNullOrEmpty(existingUser.PhoneNumber))
            {
                throw new Exception("Phonw number must exist to complete this purchase.");
            }
            // Check product stock availability
            foreach (var item in orderRequest.Items)
            {
                var product =  _productService.GetProductById(item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {item.ProductId} does not exist.");
                }
                if (product.StockQuantity < item.Quantity)
                {
                    throw new Exception($"Not enough stock for product '{product.Name}'. Available: {product.StockQuantity}, Requested: {item.Quantity}.");
                }
            }

            // Reduce stock for each purchased product
            foreach (var item in orderRequest.Items)
            {
                var product =  _productService.GetProductById(item.ProductId);
                product.StockQuantity -= item.Quantity;
                await _productService.UpdateProduct(product);
            }

            // Deduct user funds
            existingUser.Fund -= orderTotal;
             _profileService.UpdateUserAsync(existingUser);

            // Create the order
            var order = new Order
            {
                Id = _nextOrderId++,
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
            _localOrders.Add(order);

            return await Task.FromResult(order);
        }


        public async Task<Order> UpdateOrderAsync(int id, OrderUpdateRequest orderRequest)
        {
            var order = _localOrders.FirstOrDefault(o => o.Id == id);
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
            return await Task.FromResult(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = _localOrders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return await Task.FromResult(false);
            }

            _localOrders.Remove(order);
            return await Task.FromResult(true);
        }

        public async Task<List<Order>> GetAllOrdersForUserAsync(int userId)
        {
            return await Task.FromResult(_localOrders.Where(o => o.ProfileId == userId).ToList());
        }

        public async Task<Order> UpdateOrderAsync(int id, Order orderRequest)
        {
            var order = _localOrders.FirstOrDefault(o => o.Id == id);
            order.DispatchDate = orderRequest.DispatchDate;
            order.IsDispatched = orderRequest.IsDispatched;

            return await Task.FromResult(order);

        }
    }
}