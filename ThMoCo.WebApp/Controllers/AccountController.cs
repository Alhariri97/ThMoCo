using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ThMoCo.WebApp.Controllers
{
    public class AccountController : Controller
    {


        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();
            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
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
        public IActionResult Profile()
        {
            return View(new
            {
                Name = User.Identity.Name,
                EmailAddress = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                ProfileImage = User.Claims
                .FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }

    }
}
