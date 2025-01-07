namespace ThMoCo.WebApp.DTO;

public class OrderDTO
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public List<OrderItemDTO> Items { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    public DateTime? DispatchDate { get; set; }
    public bool IsDispatched { get; set; } = false;
}
