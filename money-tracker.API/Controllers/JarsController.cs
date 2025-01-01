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
    public class JarsController : BaseController
    {
        private readonly IJarsService _jarsService;

        public JarsController(IJarsService jarsService, UserManager<User> userManager)
            : base(userManager)
        {
            _jarsService = jarsService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateJar(CreateJarDto dto)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            ServiceResult result = await _jarsService.AddJar(dto, user.Id);
            if (!result.Success)
                return StatusCode(result.StatusCode ?? 400, result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJar(int id)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            ServiceResult result = await _jarsService.GetJar(user.Id, id);
            if (!result.Success)
                return StatusCode(result.StatusCode ?? 400, result);

            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetMyJar()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            ServiceResult result = await _jarsService.GetJars(user.Id);
            if (!result.Success)
                return StatusCode(result.StatusCode ?? 400, result);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateJar(UpdateJarDto dto, int id)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            ServiceResult result = await _jarsService.UpdateJar(dto, user.Id, id);
            if (!result.Success)
                return StatusCode(result.StatusCode ?? 400, result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJar(int id)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            ServiceResult result = await _jarsService.DeleteJar(user.Id, id);
            if (!result.Success)
                return StatusCode(result.StatusCode ?? 400, result);

            return Ok(result);
        }
    }
}
