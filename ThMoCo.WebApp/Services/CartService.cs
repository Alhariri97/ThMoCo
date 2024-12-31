using ThMoCo.WebApp.Models;

namespace ThMoCo.WebApp.Services;

public class CartService
{
    private readonly List<CartItem> _cartItems = new List<CartItem>();

    // Add item to cart
    public void AddToCart(ProductDTO product)
    {
        var existingItem = _cartItems.FirstOrDefault(c => c.Product.Id == product.Id);
        if (existingItem != null)
        {
            existingItem.Quantity += 1;
        }
        else
        {
            _cartItems.Add(new CartItem { Product = product, Quantity = 1 });
        }
    }

    // Get all items in the cart
    public List<CartItem> GetCartItems()
    {
        return _cartItems;
    }

    // Clear the cart
    public void ClearCart()
    {
        _cartItems.Clear();
    }

    // Get total cost of items in the cart
    public decimal GetTotal()
    {
        return _cartItems.Sum(item => item.Product.Price * item.Quantity);
    }

    // Update item quantity in the cart
    public void UpdateCartItem(int productId, int quantity)
    {
        var item = _cartItems.FirstOrDefault(c => c.Product.Id == productId);
        if (item != null)
        {
            if (quantity <= 0)
            {
                // Remove item if quantity is set to 0 or less
                _cartItems.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }
        }
    }

    // Remove an item from the cart
    public void RemoveFromCart(int productId)
    {
        var item = _cartItems.FirstOrDefault(c => c.Product.Id == productId);
        if (item != null)
        {
            _cartItems.Remove(item);
        }
    }
}
