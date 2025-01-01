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

                //await SyncUserWithDatabase(userId, email, name);
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


        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var paymentCard = await _profileService.GetPaymentCard();
            var address = await _profileService.GetAddress();

            var model = new UserProfileViewModel
            {
                Name = User.Identity.Name,
                EmailAddress = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                ProfileImage = User.Claims
                    .FirstOrDefault(c => c.Type == "picture")?.Value,

                // Payment card fields
                CardNumber = paymentCard.CardNumber,
                CardHolderName = paymentCard.CardHolderName,
                ExpiryDate = paymentCard.ExpiryDate, 
                Cvv = paymentCard.Cvv,
                // Address fields
                Street = address.Street, 
                City = address.City,
                State = address.Street, 
                PostalCode = address.PostalCode


            };

            return View(model);

        }


        [HttpPost]
        public IActionResult SavePaymentCard(UserProfileViewModel model)
        {
            // Here, model.CardNumber, model.CardHolderName, etc. will be populated.
            // Save to database or process accordingly.
            var paymentCard = new PaymentCardDTO()
            {
                CardHolderName = model.CardHolderName,
                CardNumber = model.CardNumber,
                ExpiryDate = model.ExpiryDate,
                Cvv = model.Cvv,
            };
            _profileService.SavePaymentCard(paymentCard);
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public IActionResult SaveAddress(UserProfileViewModel model)
        {
            // model.Street, model.City, etc. will be populated.
            var address = new AddressDTO()
            {
                Street = model.Street,
                City = model.City,
                State = model.State,
                PostalCode = model.PostalCode,

            };
            _profileService.SaveAddress(address);
            return RedirectToAction("Profile");
        }

    }
}
