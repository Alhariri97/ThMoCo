using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;

namespace ThMoCo.Api.Controllers;


//[Authorize]
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
        if (userDto == null || string.IsNullOrWhiteSpace(userDto.user_id))
        {
            return BadRequest("Invalid user data.");
        }

        try
        {
            // Check if the user already exists
            var existingUser =  _profileService.GetUserByAuthIdAsync(userDto.user_id);

            if (existingUser != null)
            {
                return Conflict("User already exists.");
            }

            // Map the DTO to the AppUser model
            var newUser = new AppUser
            {
                Name = userDto.name,
                Email = userDto.email,
                UserAuthId = userDto.user_id,
                PhoneNumber = userDto.phone_number,
                Role = "User", // Default role
                IsEmailVerified = userDto.email_verified,
                UpdatedAt = userDto.updated_at,
                LastLogin = null,
                Fund = 0.0m, // Default fund value
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
    /// Retrieves the current user
    /// </summary>
    /// 
    
    [HttpGet("user")]
    public ActionResult<AppUserDTO> GetUser()
    {

        try
        {
            var userId = GetCurrentUserId();

            var existingUser = _profileService.GetUserByAuthIdAsync(userId);

            if (existingUser == null)
                return NotFound("No User found for this user.");

            return Ok(existingUser);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error, Error:{ex.Message}.");

        }
        try
        {

        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error, Error:{ex.Message}.");

        }
    }

    [Authorize]
    [HttpPut("user")]
    public ActionResult<AppUserDTO> UpdateUserInfo([FromBody] AppUserDTO userDto)
    {

        if (userDto == null )
        {
            return BadRequest("Invalid user data.");
        }
        try
        {
            var userId = GetCurrentUserId();
            var existingUser = _profileService.GetUserByAuthIdAsync(userId);

            // Retrieve the existing user by Auth ID
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            // Update user properties
            existingUser.Name = userDto.Name ?? existingUser.Name;
            existingUser.PhoneNumber = userDto.PhoneNumber ?? existingUser.PhoneNumber;
            existingUser.PhotoUrl = userDto.PhotoUrl ?? existingUser.PhotoUrl;
            existingUser.UpdatedAt = DateTime.UtcNow; // Set the updated timestamp

            // Save changes to the database
            _profileService.UpdateUserAsync(existingUser);

            return Ok(existingUser);

        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, $"Internal server error, Error:{ex.Message}.");

        }
    }









    /// <summary>
    /// Retrieves the current user’s Payment Card (if any).
    /// </summary>
    [HttpGet("paymentcard")]
    public ActionResult<PaymentCardDTO> GetPaymentCard()
    {

        try
        {    
            var userId = GetCurrentUserId();

            var cardDto = _profileService.GetPaymentCard(userId);

            if (cardDto == null)
                return NotFound("No payment card found for this user.");

            // In real world, mask the card or return a safe subset of data
            return Ok(cardDto);

        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, $"Internal server error, Error:{ex.Message}.");
        }

        try
        {

        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, $"Internal server error, Error:{ex.Message}.");
        }
    }

    /// <summary>
    /// Saves or updates the current user’s Payment Card.
    /// </summary>
    [HttpPost("paymentcard")]
    public ActionResult<PaymentCardDTO> SavePaymentCard([FromBody] PaymentCardDTO cardDto)
    {

        try
        {
            var userId = GetCurrentUserId();
            var payment = _profileService.SavePaymentCard(userId, cardDto);

            Console.WriteLine("Payment card saved successfully.");

            return Ok(payment);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, $"Internal server error, Error:{ex.Message}.");
        }
    }


    /// <summary>
    /// Retrieves the current user’s Address (if any).
    /// </summary>
    [HttpGet("address")]
    public ActionResult<AddressDTO> GetAddress()
    {

        try
        {
            var userId = GetCurrentUserId();
            var addressDto = _profileService.GetAddress(userId);

            if (addressDto == null)
                return NotFound("No address found for this user.");

            return Ok(addressDto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, $"Internal server error, Error:{ex.Message}.");
        }
    }

    /// <summary>
    /// Saves or updates the current user’s Address.
    /// </summary>
    [HttpPost("address")]
    public ActionResult<AddressDTO> SaveAddress([FromBody] AddressDTO addressDto)
    {

        try
        {
            var userId = GetCurrentUserId();
            var updatedCard = _profileService.SaveAddress(userId, addressDto);

            Console.WriteLine("Address saved successfully.");
            return Ok(updatedCard);

        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, $"Internal server error, Error:{ex.Message}.");
        }

    }










    [HttpPost("addfunds")]
    public async Task<ActionResult> AddFunds([FromBody] AddFundsDTO addFundsDto)
    {
        if (addFundsDto == null || addFundsDto.Amount <= 0)
        {
            return BadRequest("Invalid fund amount.");
        }

        try
        {
            var userId = GetCurrentUserId();

            // Retrieve the existing user by Auth ID
            var existingUser = _profileService.GetUserByAuthIdAsync(userId);

            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            // Check if the user has a payment card
            var paymentCard = _profileService.GetPaymentCard(userId);
            if (paymentCard == null ||
                 string.IsNullOrWhiteSpace(paymentCard.CardNumber) ||
                 string.IsNullOrWhiteSpace(paymentCard.CardHolderName) ||
                 string.IsNullOrWhiteSpace(paymentCard.ExpiryDate) ||
                 paymentCard.Cvv == null)
            {
                return BadRequest("Card info needs verifying: information is incomplete.");
            }

            // Validate CVV

            if (addFundsDto.Cvv != paymentCard.Cvv)
            {
                return StatusCode(400, "Wrong Cvv.");
            }



            // Update the user's fund balance
            existingUser.Fund = (existingUser.Fund ?? 0) + addFundsDto.Amount;
            existingUser.UpdatedAt = DateTime.UtcNow; // Update the timestamp

            // Save changes to the database
             var updatedUser = _profileService.UpdateUserAsync(existingUser);

            return Ok(updatedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error adding funds: {ex.Message}");
            return StatusCode(500, $"Internal server error: {ex.Message}.");
        }
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
