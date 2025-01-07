using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;

namespace ThMoCo.Api.Controllers;

[Authorize(Roles = "admin")]
[Authorize] 
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    /// <summary>
    /// Returns a list of orders that are ready to be dispatched.
    /// </summary>
    [HttpGet("customers")]
    public async Task<IActionResult> GetOrders()
    {
        var customers = await _adminService.GetCustomers();
        return Ok(customers);
    }
    /// <summary>
    /// Returns a list of users.
    /// </summary>
    [HttpGet("orders")]
    public async Task<IActionResult> GetUsers()
    {
        var orders = await _adminService.GetOrders();
        return Ok(orders);
    }
    /// <summary>
    /// Returns a list of orders that are ready to be dispatched.
    /// </summary>
    [HttpGet("orders/dispatch")]
    public async Task<IActionResult> GetOrdersToDispatch()
    {
        var orders = await _adminService.GetOrdersToDispatchAsync();
        return Ok(orders);
    }

    /// <summary>
    /// Marks a specific order as dispatched.
    /// </summary>
    /// <param name="orderId">The ID of the order to mark as dispatched.</param>
    [HttpPut("orders/{orderId}/dispatch")]
    public async Task<IActionResult> MarkOrderAsDispatched(int orderId)
    {
        var result = await _adminService.MarkOrderAsDispatchedAsync(orderId);
        if (!result)
        {
            return NotFound($"Order with ID {orderId} not found or already dispatched.");
        }
        return Ok($"Order {orderId} marked as dispatched.");
    }

    /// <summary>
    /// Retrieves a customer's profile based on their ID.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    [HttpGet("customers/{customerId}")]
    public async Task<IActionResult> GetCustomerProfile(int customerId)
    {
        var customer = await _adminService.GetCustomerProfileAsync(customerId);
        if (customer == null)
        {
            return NotFound($"Customer with ID {customerId} not found.");
        }
        return Ok(customer);
    }

    /// <summary>
    /// Deletes a customer's account.
    /// </summary>
    /// <param name="customerId">The ID of the customer to delete.</param>
    [HttpDelete("customers/{customerId}")]
    public async Task<IActionResult> DeleteCustomerAccount(int customerId)
    {
        var result = await _adminService.DeleteCustomerAccountAsync(customerId);
        if (!result)
        {
            return NotFound($"Customer with ID {customerId} not found or could not be deleted.");
        }
        return Ok($"Customer {customerId} account deleted successfully.");
    }

    /// <summary>
    /// Admin secret test endpoint.
    /// </summary>
    [HttpGet("secret")]
    public IActionResult GetSecret()
    {
        return Ok("You are an admin!");
    }
}
