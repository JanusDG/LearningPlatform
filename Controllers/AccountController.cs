using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearningPlatform.Data.Service;
using LearningPlatform.Models;



namespace LearningPlatform.Controllers
{ 
    
    public class AccountController : Controller
    {
        private readonly JwtService _jwtService;

        public AccountController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest)
        {
            var response = await _jwtService.AuthenticateAsync(loginRequest);
            if (response is null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }
    }
}