
using Microsoft.AspNetCore.Mvc;
using ThMoCo.WebApp.IServices;
using ThMoCo.WebApp.Models;
using ThMoCo.WebApp.Services;

namespace ThMoCo.WebApp.Controllers;

public class CartController : Controller
{
    private readonly CartService _cartService;
    private readonly IProductService _productService;

    public CartController(CartService cartService, IProductService productService)
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
    public async Task<IActionResult> AddToCart(int productid)
    {
        ProductDTO productDTO = await _productService.GetProductByIdAsync(productid);
        _cartService.AddToCart(productDTO);
        return RedirectToAction("Index");
    }


    [HttpPost]
    public IActionResult UpdateCart(int productId, int quantity)
    {
        _cartService.UpdateCartItem(productId, quantity);
        return RedirectToAction("Index");
    }


    [HttpPost]
    public IActionResult RemoveFromCart(int productId)
    {
        _cartService.RemoveFromCart(productId);
        return RedirectToAction("Index");
    }

}
