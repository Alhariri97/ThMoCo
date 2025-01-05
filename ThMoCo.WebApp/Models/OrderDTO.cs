namespace ThMoCo.WebApp.Models;

public class OrderDTO
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public List<OrderItemDTO> Items { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }

}
