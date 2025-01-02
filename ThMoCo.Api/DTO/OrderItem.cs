namespace ThMoCo.Api.DTO;

public class OrderItem
{
    public int ProductId { get; set; } // The ID of the product
    public string ProductName { get; set; } // Name of the product
    public int Quantity { get; set; } // Number of this product being ordered
    public decimal PricePerUnit { get; set; } // Price per unit of the product
}