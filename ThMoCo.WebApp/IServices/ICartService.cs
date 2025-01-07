using ThMoCo.WebApp.DTO;
using ThMoCo.WebApp.Models;

namespace ThMoCo.WebApp.IServices;

public interface ICartService
{
    /// <summary>
    /// Adds a product to the cart.
    /// </summary>
    /// <param name="product">The product to add.</param>
    void AddToCart(ProductDTO product);

    /// <summary>
    /// Gets all items currently in the cart.
    /// </summary>
    /// <returns>A list of cart items.</returns>
    List<CartItem> GetCartItems();

    /// <summary>
    /// Clears all items from the cart.
    /// </summary>
    void ClearCart();

    /// <summary>
    /// Gets the total cost of items in the cart.
    /// </summary>
    /// <returns>The total cost.</returns>
    decimal GetTotal();

    /// <summary>
    /// Updates the quantity of a specific item in the cart.
    /// </summary>
    /// <param name="productId">The ID of the product to update.</param>
    /// <param name="quantity">The new quantity.</param>
    void UpdateCartItem(int productId, int quantity);

    /// <summary>
    /// Removes an item from the cart.
    /// </summary>
    /// <param name="productId">The ID of the product to remove.</param>
    void RemoveFromCart(int productId);
}

