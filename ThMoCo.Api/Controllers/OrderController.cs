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
    [Authorize(Roles = "admin")] // Only admins can get all orders for all users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    // Get all orders for a user.

    [HttpGet("user/{id}")] // Adjust this route
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int id)
    {
        var userId = GetCurrentUserId();
        var existingUser = _profileService.GetUserByAuthIdAsync(userId);

        if (existingUser == null)
        {
            return NotFound("User not found.");
        }
        if (existingUser.Id != id )
        {
            return BadRequest("User Id does not match the db user's matches.");
        }


        var orders = await _orderService.GetAllOrdersForUserAsync(existingUser.Id);
        return Ok(orders);
    }


    // Get order by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderById(int id)
    {
        var userId = GetCurrentUserId();
        var existingUser = _profileService.GetUserByAuthIdAsync(userId);
        if ( existingUser.Id != id )
        {
            return BadRequest("User id is not same as the user's id in db");
        }

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
        var userId = GetCurrentUserId();
        var existingUser = _profileService.GetUserByAuthIdAsync(userId);
        if(orderRequest.ProfileId != existingUser.Id)
        {
            return BadRequest("User id is not same as the user's id in db");
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





    /// <summary>
    /// Helper method to retrieve the current user’s ID
    /// based on the NameIdentifier claim.
    /// </summary>
    private string GetCurrentUserId()
    {
        try
        {
            // Attempt to retrieve the user ID claim
            var userIdClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"); // Use the 'sub' claim

            // Validate the claim
            if (userIdClaim == null || string.IsNullOrWhiteSpace(userIdClaim.Value))
            {
                throw new Exception("User ID claim is missing or invalid.");
            }

            return userIdClaim.Value;
        }
        catch (Exception ex)
        {
            // Log or handle the error if necessary
            throw new Exception("Error extracting User ID from claims.", ex);
        }
    }
}
