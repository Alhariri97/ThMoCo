using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;

namespace ThMoCo.Api.Controllers;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }



    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> RegisterUser([FromBody] RegisterUserDTO userDto)
    {
        if (userDto == null || string.IsNullOrWhiteSpace(userDto.UserId))
        {
            return BadRequest("Invalid user data.");
        }

        try
        {
            // Check if the user already exists
            var existingUser =  _profileService.GetUserByAuthIdAsync(userDto.UserId);

            if (existingUser != null)
            {
                return Conflict("User already exists.");
            }

            // Map the DTO to the AppUser model
            var newUser = new AppUser
            {
                Name = userDto.Name,
                Email = userDto.Email,
                UserAuthId = userDto.UserId,
                PhotoUrl = userDto.Picture,
                PhoneNumber = userDto.PhoneNumber,
                Role = "User", // Default role
                IsEmailVerified = userDto.EmailVerified,
                UpdatedAt = userDto.UpdatedAt,
                LastLogin = null,
                Fund = 0.0, // Default fund value
            };

            // Save the new user to the database
            var createdUser = _profileService.AddUserAsync(newUser);

            // Return the created user
            return Ok();
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.Error.WriteLine($"Error registering user: {ex.Message}");

            return StatusCode(500, "Internal server error.");
        }
    }












    /// <summary>
    /// Retrieves the current user’s Payment Card (if any).
    /// </summary>
    [HttpGet("paymentcard")]
    public ActionResult<PaymentCardDTO> GetPaymentCard()
    {
        // Get current user ID from claims (assuming the NameIdentifier claim is your UserId).
        var userId = GetCurrentUserId();

        var cardDto = _profileService.GetPaymentCard(userId);

        if (cardDto == null)
            return NotFound("No payment card found for this user.");

        // In real world, mask the card or return a safe subset of data
        return Ok(cardDto);
    }

    /// <summary>
    /// Saves or updates the current user’s Payment Card.
    /// </summary>
    [HttpPost("paymentcard")]
    public ActionResult<PaymentCardDTO> SavePaymentCard([FromBody] PaymentCardDTO cardDto)
    {
        var userId = GetCurrentUserId();
        var payment = _profileService.SavePaymentCard(userId, cardDto);

        Console.WriteLine("Payment card saved successfully.");

        return Ok(payment);
    }


    /// <summary>
    /// Retrieves the current user’s Address (if any).
    /// </summary>
    [HttpGet("address")]
    public ActionResult<AddressDTO> GetAddress()
    {
        var userId = GetCurrentUserId();
        var addressDto = _profileService.GetAddress(userId);

        if (addressDto == null)
            return NotFound("No address found for this user.");

        return Ok(addressDto);
    }

    /// <summary>
    /// Saves or updates the current user’s Address.
    /// </summary>
    [HttpPost("address")]
    public ActionResult<AddressDTO> SaveAddress([FromBody] AddressDTO addressDto)
    {
        var userId = GetCurrentUserId();
       var updatedCard =  _profileService.SaveAddress(userId, addressDto);

        Console.WriteLine("Address saved successfully.");
        return Ok(updatedCard);

    }

    /// <summary>
    /// Helper method to retrieve the current user’s ID
    /// based on the NameIdentifier claim.
    /// </summary>
    private string GetCurrentUserId()
    {
        // This assumes you're using standard .NET Identity or a similar approach
        // If you're using JWT, you might get the user ID from a different claim.
        var userIdClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");// Use the 'sub' claim
        return userIdClaim != null ? userIdClaim.Value : string.Empty;
    }
}
