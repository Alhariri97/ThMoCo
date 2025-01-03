
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;

namespace ThMoCo.Api.Services
{
    public class LocalOrderService : IOrderService
    {
        private readonly List<Order> _localOrders;
        private int _nextOrderId;

        public LocalOrderService()
        {
            _nextOrderId = 3; // Start with 3 as two orders are pre-populated
            _localOrders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    ProfileId = 101,
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
            var order = new Order
            {
                Id = _nextOrderId++,
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

        public Task<List<Order>> GetAllOrdersForUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}