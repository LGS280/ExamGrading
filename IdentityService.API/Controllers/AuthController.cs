using IdentityService.Business.Interfaces;
using IdentityService.Business.ServiceModels.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerDto)
        {
            var result = await _authService.RegisterUserAsync(registerDto);
            if (!result)
            {
                return BadRequest("Username already exists.");
            }
            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginDto)
        {
            var token = await _authService.LoginUserAsync(loginDto);
            if (token == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(new { token = token });
        }
    }
}
