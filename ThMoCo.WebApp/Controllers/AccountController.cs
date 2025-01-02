using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ThMoCo.WebApp.Models;
using ThMoCo.WebApp.IServices;
using System.IO;

namespace ThMoCo.WebApp.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IProfileService _profileService;
        private readonly string _baseAddress;

        public AccountController(IHttpClientFactory httpClientFactory,
            IConfiguration configuration, IProfileService profileService) 
        {
            _httpClientFactory = httpClientFactory;
            _baseAddress = configuration["Values:BaseAddress"];
            _profileService = profileService;

        }

        [AllowAnonymous]
        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var name = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

                // Fetch additional user data from your service
                var user = await _profileService.GetUser();

                // Assume user.PhotoUrl contains the photo URL
                var userPhotoFromDatabase = user.PhotoUrl;

                // Use the helper method to add or update the PhotoUrl claim
                //var updatedPrincipal = AddOrUpdatePhotoUrlClaim(User, userPhotoFromDatabase);

                // Refresh the user's authentication context with the updated claims
                //await HttpContext.SignInAsync(Auth0Constants.AuthenticationScheme, updatedPrincipal);
            }
        }





        public async Task<IActionResult> Logout()
        {


            await HttpContext.SignOutAsync(
            Auth0Constants.AuthenticationScheme,
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")
            });

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var userInfo = await _profileService.GetUser();
                var photo = "";
                if (userInfo.PhotoUrl != null)
                {
                    photo = userInfo.PhotoUrl;
                }
                else
                {
                    photo = User.Claims
                        .FirstOrDefault(c => c.Type == "picture")?.Value;
                }
                var model = new UserProfileViewModel
                {
                    Id = userInfo.Id,
                    Name = userInfo.Name,
                    EmailAddress = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    ProfileImage = photo,
                    Email = userInfo.Email,
                    PhoneNumber = userInfo.PhoneNumber,
                    Fund = userInfo.Fund,
                    UpdatedAt = userInfo.UpdatedAt,
                    LastLogin = userInfo.LastLogin,
                    Provider = userInfo.Provider,
                    Role = userInfo.Role,
                    IsEmailVerified = userInfo.IsEmailVerified,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching profile: {ex.Message}");
                return RedirectToAction("Logout", "Account");
            }

        }


        [HttpGet("profile/update")]
        public async Task<IActionResult> UpdateProfile()
        {
            var userInfo = await _profileService.GetUser();

            //PhotoUrl = userInfo.PhotoUrl,

            var model = new UserProfileViewModel
            {
                Id = userInfo.Id,
                Name = userInfo.Name,
                EmailAddress = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,

                ProfileImage = userInfo.PhotoUrl,

                Email = userInfo.Email,
                PhoneNumber = userInfo.PhoneNumber,
                Fund = userInfo.Fund,
                UpdatedAt = userInfo.UpdatedAt,
                LastLogin = userInfo.LastLogin,
                Provider = userInfo.Provider,
                Role = userInfo.Role,
                IsEmailVerified = userInfo.IsEmailVerified,
            };

            return View(model);
        }


        [HttpPost("profile/update")]
        public async Task<IActionResult> UpdateProfile(UpdateUserProfileViewModel model, IFormFile Photo)
        {
            try
            {
                // Fetch the user information
                var userInfo = await _profileService.GetUser();

                if (userInfo == null)
                {
                    ModelState.AddModelError("", "User not found.");
                    return View("EditProfile", model);
                }



                // Handle file upload if a new photo is provided
                // Handle file upload if a new photo is provided
                if (Photo != null && Photo.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                    // Ensure the directory exists
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(Photo.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Photo.CopyToAsync(fileStream);
                    }

                    // Update photo URL
                    model.PhotoUrl = $"/uploads/{uniqueFileName}";
                    userInfo.PhotoUrl = model.PhotoUrl;

                    // Update the user's claims with the new photo URL
                    //var updatedPrincipal = AddOrUpdatePhotoUrlClaim(User, model.PhotoUrl);

                    //// Refresh the authentication context with the Cookies scheme
                    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, updatedPrincipal);
                }


                // Update user properties
                if (!string.IsNullOrWhiteSpace(model.Name) && model.Name != userInfo.Name)
                {
                    userInfo.Name = model.Name;
                }

                if (!string.IsNullOrWhiteSpace(model.PhoneNumber) && model.PhoneNumber != userInfo.PhoneNumber)
                {
                    userInfo.PhoneNumber = model.PhoneNumber;
                }

                // Save updated user info
                await _profileService.SaveUser(userInfo);

                // Redirect to the profile view
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Console.Error.WriteLine($"Error updating profile: {ex.Message}");

                // Add a model error and return to the edit view
                ModelState.AddModelError("", "An error occurred while updating the profile. Please try again.");
                return View("EditProfile", model);
            }
        }



        [HttpGet("profile/Payment")]
        public async Task<IActionResult> Payment()
        {
            var paymentCard = await _profileService.GetPaymentCard();

            return View(paymentCard);
        }

        [HttpPost]
        public IActionResult SavePaymentCard(PaymentCardDTO model)
        {


            _profileService.SavePaymentCard(model);
            return RedirectToAction("profile");
        }

        [HttpGet("profile/Address")]
        public async Task<IActionResult> Address()
        {
            var address = await _profileService.GetAddress();

            return View(address);
        }
        [HttpPost]
        public async Task<IActionResult> SaveAddress(AddressDTO model)
        {
            var address = new AddressDTO()
            {
                Street = model.Street,
                City = model.City,
                State = model.State,
                PostalCode = model.PostalCode,

            };
            _profileService.SaveAddress(address);
            return RedirectToAction("profile");
        }


        [HttpPost("profile/AddFunds")]
        public async Task<IActionResult> AddFunds([FromBody] AddFundsDTO fund)
        {
            try
            {
                var paymentCard = await _profileService.AddFund(fund);

                return Ok(new { message = "Funds added successfully." });

            }
            catch (Exception ex)
            {
                return NotFound("Error Adding Funds.");
            }

        }



        //private ClaimsPrincipal AddOrUpdatePhotoUrlClaim(ClaimsPrincipal userPrincipal, string photoUrl)
        //{
        //    if (userPrincipal == null || string.IsNullOrEmpty(photoUrl))
        //    {
        //        return userPrincipal; // No changes needed
        //    }

        //    var claims = userPrincipal.Claims.ToList();

        //    // Add or update the PhotoUrl claim
        //    var photoClaim = claims.FirstOrDefault(c => c.Type == "PhotoUrl");
        //    if (photoClaim != null)
        //    {
        //        claims.Remove(photoClaim); // Remove existing claim if it exists
        //    }
        //    claims.Add(new Claim("PhotoUrl", photoUrl)); // Add the updated claim

        //    // Recreate the ClaimsPrincipal with the updated claims
        //    var claimsIdentity = new ClaimsIdentity(claims, userPrincipal.Identity?.AuthenticationType);
        //    return new ClaimsPrincipal(claimsIdentity);
        //}

    }


}


