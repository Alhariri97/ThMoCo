namespace ThMoCo.Api.Models;


public class Order
{
    public int Id { get; set; }
    public int ProfileId { get; set; } // The ID of the user placing the order
    public List<OrderItem> Items { get; set; } = new List<OrderItem>(); // List of products in the order
    public decimal TotalAmount { get; set; } // Total cost of the order
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DispatchDate { get; set; }
    public bool IsDispatched { get; set; } = false;
    public void CalculateTotalAmount()
    {
        TotalAmount = 0;
        foreach (var item in Items)
        {
            TotalAmount += item.Quantity * item.PricePerUnit;
        }
    }
}