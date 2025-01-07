using Microsoft.AspNetCore.Mvc;
using ThMoCo.WebApp.IServices;
using ThMoCo.WebApp.Models;
using ThMoCo.WebApp.DTO;


namespace ThMoCo.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProfileService _profileService;

        private IProductService _productService;
        public OrderController(IOrderService orderService,
            IProfileService profileService,
            IProductService productService)
        {
            _orderService = orderService;
            _profileService = profileService;

            _productService = productService;
        }

        // GET: /Order/
        public async Task<IActionResult> Index()
        {
            var userInfo = await _profileService.GetUser();

            var orders = await _orderService.GetAllOrdersForUserAsync(userInfo.Id);
            return View(orders);
        }

        // GET: /Order/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: /Order/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateRequest model)
        {

            // Get the current user's cart
            var cart =  model.Items;

            // Check if the cart is empty
            if (cart == null || !cart.Any())
            {
                 TempData["Error"] = "Your cart is empty. Add items to your cart before proceeding to checkout.";
                return RedirectToAction("Index", "Cart");
            }

            // Validate funds
            var userInfo = await _profileService.GetUser();
            if (userInfo == null)
            {
                TempData["Error"] = "Error user is not found!.";
                return RedirectToAction("Index", "Cart");

            }
            var cartTotal = cart.Sum(item => item.PricePerUnit * item.Quantity);
            if (!userInfo.Fund.HasValue || userInfo.Fund.Value < cartTotal)
            {
                TempData["Error"] = "Insufficient funds to complete this purchase.";
                return RedirectToAction("Index", "Cart");
            }

            // Validate stock
            foreach (var item in cart)
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId);
                if (product.StockQuantity < item.Quantity)
                {
                    TempData["Error"] = $"The product '{item.ProductName}' does not have enough stock to fulfill your order. Available quantity: {product.StockQuantity}.";
                    return RedirectToAction("Index", "Cart");
                }
            }



            // Create the order
            var order = new OrderCreateRequest
            {
                ProfileId = userInfo.Id,
                Items = cart.Select(c => new OrderItemDTO
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    PricePerUnit = c.PricePerUnit,
                    ProductName = c.ProductName,
                }).ToList(),
            };

            await _orderService.CreateOrderAsync(order);

            // Clear the cart

            TempData["Success"] = "Your order has been placed successfully!, Go to orders to see it.";
            return RedirectToAction("Index", "Cart");
        }





        //// GET: /Order/Edit/{id}
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var order = await _orderService.GetOrderByIdAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    var editModel = new OrderUpdateRequest
        //    {
        //        Id = order.Id,
        //        Name = order.Name,
        //        Quantity = order.Quantity
        //    };

        //    return View(editModel);
        //}

        //// POST: /Order/Edit/{id}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, OrderEditModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var updatedOrder = await _orderService.UpdateOrderAsync(id, model);
        //    if (updatedOrder == null)
        //    {
        //        return NotFound();
        //    }

        //    return RedirectToAction(nameof(Index));
        //}

        // GET: /Order/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: /Order/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _orderService.DeleteOrderAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
