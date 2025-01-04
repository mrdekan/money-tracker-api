using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using money_tracker.Application.Dtos.Requests.Jar;
using money_tracker.Application.Interfaces;
using money_tracker.Application.Services;
using money_tracker.Domain.Entities;

namespace money_tracker.API.Controllers
{
    [Route("api/v1/jars")]
    [ApiController]
    [Authorize]
    public class JarsController(IJarsService jarsService, UserManager<User> userManager)
        : BaseController(userManager)
    {
        private readonly IJarsService _jarsService = jarsService;

        [HttpPost("")]
        public async Task<IActionResult> CreateJar(CreateJarDto dto)
        {
            var userId = await GetUserIdAsync();

            ServiceResult result = await _jarsService.AddJar(dto, userId);
            return GenerateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJar(int id)
        {
            var userId = await GetUserIdAsync();

            ServiceResult result = await _jarsService.GetJar(userId, id);
            return GenerateResponse(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetMyJar()
        {
            var userId = await GetUserIdAsync();

            ServiceResult result = await _jarsService.GetJars(userId);
            return GenerateResponse(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateJar(UpdateJarDto dto, int id)
        {
            var userId = await GetUserIdAsync();

            ServiceResult result = await _jarsService.UpdateJar(dto, userId, id);
            return GenerateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJar(int id)
        {
            var userId = await GetUserIdAsync();

            ServiceResult result = await _jarsService.DeleteJar(userId, id);
            return GenerateResponse(result);
        }
    }
}
