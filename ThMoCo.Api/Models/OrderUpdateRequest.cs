using ThMoCo.Api.DTO;

namespace ThMoCo.Api.Models;

public class OrderUpdateRequest
{
    public List<OrderItemDTO> Items { get; set; }
}
