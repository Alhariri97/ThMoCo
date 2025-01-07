namespace ThMoCo.WebApp.DTO;

public class OrderItemDTO
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
}