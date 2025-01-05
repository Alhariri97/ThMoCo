namespace ThMoCo.WebApp.Models;

public class OrderCreateRequest
{
    public int ProfileId { get; set; }
    public List<OrderItemDTO> Items { get; set; }
}
