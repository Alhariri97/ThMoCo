using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ThMoCo.WebApp.IServices;
using ThMoCo.WebApp.Models;

public class CartService 
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISession _session;
    private const string CartSessionKey = "Cart";

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _session = _httpContextAccessor.HttpContext.Session;
    }

    // Get Cart from Session
    public List<CartItem> GetCartItems()
    {
        var cartJson = _session.GetString(CartSessionKey);
        return cartJson != null ? JsonConvert.DeserializeObject<List<CartItem>>(cartJson) : new List<CartItem>();
    }

    // Save Cart to Session
    private void SaveCart(List<CartItem> cart)
    {
        _session.SetString(CartSessionKey, JsonConvert.SerializeObject(cart));
    }

    // Add Item to Cart
    public void AddToCart(ProductDTO product)
    {
        var cart = GetCartItems();
        var existingItem = cart.FirstOrDefault(c => c.Product.Id == product.Id);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            cart.Add(new CartItem { Product = product, Quantity = 1 });
        }

        SaveCart(cart);
    }

    // Update Item Quantity
    public void UpdateCartItem(int productId, int quantity)
    {
        var cart = GetCartItems();
        var item = cart.FirstOrDefault(c => c.Product.Id == productId);

        if (item != null)
        {
            item.Quantity = quantity > 0 ? quantity : 1; // Prevents negative quantities
        }

        SaveCart(cart);
    }

    // Remove Item from Cart
    public void RemoveFromCart(int productId)
    {
        var cart = GetCartItems();
        cart.RemoveAll(c => c.Product.Id == productId);
        SaveCart(cart);
    }

    // Clear Cart
    public void ClearCart()
    {
        _session.Remove(CartSessionKey);
    }

    public decimal GetTotal()
    {
        throw new NotImplementedException();
    }
}
