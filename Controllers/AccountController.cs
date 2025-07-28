using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearningPlatform.Data.Service;
using LearningPlatform.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Helpers;


namespace LearningPlatform.Controllers
{

    public class AccountController : Controller
    {
        private readonly IJwtService _jwtService;

        public AccountController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest)
        {
            var response = await _jwtService.AuthenticateAsync(loginRequest);
            if (response is null)
            {
                return Unauthorized();
            }

            //Temporary debugging line
            Console.WriteLine($"YOU ARE LOGGED IN AS: {response.Username} WITH ROLE: {response.Role}");

            var accessToken = response.AccessToken;

            // HTTP-only cookie
            Response.Cookies.Append("jwt", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            TempData["Error"] = "You do not have permission to access this resource.";
            return RedirectToAction("Error403", "Home"); // or return View() to render directly
        }


        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return RedirectToAction("Login", "Account");
        }       

    }
}