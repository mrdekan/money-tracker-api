using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using money_tracker.Application.Dtos.Requests.Stores;
using money_tracker.Application.Interfaces;
using money_tracker.Application.Services;
using money_tracker.Domain.Entities;

namespace money_tracker.API.Controllers
{
    [Route("api/v1/jars/{jarId}/stores")]
    [ApiController]
    [Authorize]
    public class StoresController : BaseController
    {
        private readonly IStoresService _storesService;

        public StoresController(IStoresService storesService, UserManager<User> userManager) : base(userManager)
        {
            _storesService = storesService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateStore(CreateStoreDto dto, int jarId)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }
            ServiceResult result = await _storesService.AddStore(dto, user.Id, jarId);

            if (!result.Success)
                return StatusCode(result.StatusCode ?? 400, result.ErrorMessage);

            return Ok();
        }
    }
}
