namespace ThMoCo.WebApp.Models
{
    public class CartItem
    {
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Product.Price * Quantity;
    }
}
