using Microsoft.AspNetCore.Mvc;
using money_tracker.Application.Dtos.Requests.Auth;
using money_tracker.Application.Interfaces;

namespace money_tracker.API.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> Login([FromBody] SignInDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
            {
                return StatusCode(result.StatusCode ?? 400, result);
            }

            return Ok(result);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> Register([FromBody] SignUpDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (!result.Success)
            {
                return StatusCode(result.StatusCode ?? 400, result);
            }

            return Ok(result);
        }
    }
}
