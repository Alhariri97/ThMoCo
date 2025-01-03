using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;


namespace ThMoCo.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IProfileService _profileService;
    private readonly IProductService _productService;

    public OrderController(IOrderService orderService,
        IProfileService profileService,
        IProductService productService)
    {
        _orderService = orderService;
        _profileService = profileService;
        _productService = productService;
    }

    // Get all orders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    // Get order by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound($"Order with ID {id} not found.");
        }
        return Ok(order);
    }

    // Create a new order
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderCreateRequest orderRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var order = await _orderService.CreateOrderAsync(orderRequest);
        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
    }

    // Update an order
    [HttpPut("{id}")]
    public async Task<ActionResult<Order>> UpdateOrder(int id, [FromBody] OrderUpdateRequest orderRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedOrder = await _orderService.UpdateOrderAsync(id, orderRequest);
        if (updatedOrder == null)
        {
            return NotFound($"Order with ID {id} not found.");
        }

        return Ok(updatedOrder);
    }

    // Delete an order
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        var success = await _orderService.DeleteOrderAsync(id);
        if (!success)
        {
            return NotFound($"Order with ID {id} not found.");
        }

        return NoContent();
    }
}
