using Microsoft.AspNetCore.Mvc;
using ThMoCo.WebApp.IServices;
using ThMoCo.WebApp.Services;
using System.Threading.Tasks;
using ThMoCo.WebApp.DTO;

namespace ThMoCo.WebApp.Controllers
{
    public class CartController : Controller
    {

        private readonly CartService _cartService;
        private readonly IProductService _productService;

        public CartController(CartService cartService,
            IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        // GET: Cart
        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            return View(cartItems);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            ProductDTO productDTO = await _productService.GetProductByIdAsync(productId);
            _cartService.AddToCart(productDTO);
            return RedirectToAction("Index");
        }

        // POST: Cart/UpdateCart
        [HttpPost]
        public IActionResult UpdateCart(int productId, int quantity)
        {
            _cartService.UpdateCartItem(productId, quantity);
            return RedirectToAction("Index");
        }

        // POST: Cart/RemoveFromCart
        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }
        public void ClearCart()
        {
            _cartService.ClearCart();
        }
    }
}
