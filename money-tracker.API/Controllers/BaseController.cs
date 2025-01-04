using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using money_tracker.Application.Exceptions;
using money_tracker.Application.Services;
using money_tracker.Domain.Entities;

namespace money_tracker.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly UserManager<User> _userManager;

        public BaseController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected async Task<User> GetCurrentUserAsync()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByEmailAsync(currentUserName);
            if (user == null)
            {
                throw new UnauthorizedUserException();
            }
            return user;
        }

        protected async Task<int> GetUserIdAsync()
        {
            var user = await GetCurrentUserAsync();
            return user?.Id ?? throw new UnauthorizedUserException();
        }

        protected IActionResult GenerateResponse(ServiceResult result)
        {
            if (!result.Success)
                return StatusCode(result.StatusCode ?? 400, result);

            return Ok(result);
        }
    }
}
