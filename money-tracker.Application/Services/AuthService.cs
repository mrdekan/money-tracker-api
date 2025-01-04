using Microsoft.AspNetCore.Identity;
using money_tracker.Application.Dtos.Requests.Auth;
using money_tracker.Application.Interfaces;
using money_tracker.Domain.Entities;

namespace money_tracker.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<User> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<ServiceResult> LoginAsync(SignInDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return ServiceResult.Fail("Invalid credentials", HttpCodes.Unauthorized);
            }

            var token = _jwtService.GenerateJwtToken(user);
            return ServiceResult.Ok(new { Token = token });
        }

        public async Task<ServiceResult> RegisterAsync(SignUpDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
            {
                return ServiceResult.Fail("Email already registered.", HttpCodes.Conflict);
            }

            var user = new User
            {
                Email = dto.Email,
                UserName = dto.Email.Split("@")[0] + Guid.NewGuid(),
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return ServiceResult.Fail(
                    string.Join(", ", result.Errors.Select(e => e.Description)),
                    HttpCodes.BadRequest
                );
            }

            var token = _jwtService.GenerateJwtToken(user);
            return ServiceResult.Ok(new { Token = token });
        }
    }
}
