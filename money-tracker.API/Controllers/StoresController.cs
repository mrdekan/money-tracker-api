using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using money_tracker.Application.Dtos.Requests.Stores;
using money_tracker.Application.Interfaces;
using money_tracker.Application.Services;
using money_tracker.Domain.Entities;

namespace money_tracker.API.Controllers
{
    [Route("api/v1/stores")]
    [ApiController]
    [Authorize]
    public class StoresController(IStoresService storesService, UserManager<User> userManager)
        : BaseController(userManager)
    {
        private readonly IStoresService _storesService = storesService;

        [HttpPost("")]
        public async Task<IActionResult> CreateStore(CreateStoreDto dto)
        {
            var userId = await GetUserIdAsync();

            ServiceResult result = await _storesService.AddStore(dto, userId);
            return GenerateResponse(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStore(UpdateStoreDto dto, int id)
        {
            var userId = await GetUserIdAsync();

            ServiceResult result = await _storesService.UpdateStore(dto, userId, id);
            return GenerateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var userId = await GetUserIdAsync();

            ServiceResult result = await _storesService.DeleteStore(userId, id);
            return GenerateResponse(result);
        }
    }
}
