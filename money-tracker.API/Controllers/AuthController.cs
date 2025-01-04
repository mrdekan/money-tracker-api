using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using money_tracker.Application.Dtos.Requests.Auth;
using money_tracker.Application.Interfaces;
using money_tracker.Application.Services;
using money_tracker.Domain.Entities;

namespace money_tracker.API.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController(IAuthService authService, UserManager<User> userManager)
        : BaseController(userManager)
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("sign-in")]
        public async Task<IActionResult> Login([FromBody] SignInDto dto)
        {
            ServiceResult result = await _authService.LoginAsync(dto);
            return GenerateResponse(result);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> Register([FromBody] SignUpDto dto)
        {
            ServiceResult result = await _authService.RegisterAsync(dto);
            return GenerateResponse(result);
        }
    }
}
