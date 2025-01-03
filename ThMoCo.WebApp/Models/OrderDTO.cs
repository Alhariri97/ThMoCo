namespace ThMoCo.WebApp.Models;

public class OrderDTO
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public List<OrderItemDto> Items { get; set; }
    public decimal TotalAmount { get; set; }
}
