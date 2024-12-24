﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace ThMoCo.App.Controllers;


public class AccountController : Controller
{
    [HttpGet("login")]
    public async Task Login(string returnUrl = "https://localhost:7084")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
          // Indicate here where Auth0 should redirect the user after a login.
          // Note that the resulting absolute Uri must be added to the
          // **Allowed Callback URLs** settings for the app.
          .WithRedirectUri(returnUrl)
          .Build();

        await HttpContext.ChallengeAsync(
          Auth0Constants.AuthenticationScheme,
          authenticationProperties
        );
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

    [Authorize]
    [HttpPost("logout")]
    public async Task Logout()
    {
        var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
          // Indicate here where Auth0 should redirect the user after a logout.
          // Note that the resulting absolute Uri must be added to the
          // **Allowed Logout URLs** settings for the app.
          .WithRedirectUri(Url.Action("Index", "Home"))
          .Build();

        // Logout from Auth0
        await HttpContext.SignOutAsync(
          Auth0Constants.AuthenticationScheme,
          authenticationProperties
        );
        // Logout from the application
        await HttpContext.SignOutAsync(
          CookieAuthenticationDefaults.AuthenticationScheme
        );
    }
}