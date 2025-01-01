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
    public class StoresController : BaseController
    {
        private readonly IStoresService _storesService;
        private readonly ITransactionsService _transactionsService;

        public StoresController(IStoresService storesService, ITransactionsService transactionsService, UserManager<User> userManager) : base(userManager)
        {
            _storesService = storesService;
            _transactionsService = transactionsService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateStore(CreateStoreDto dto)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }
            ServiceResult result = await _storesService.AddStore(dto, user.Id);

            if (!result.Success)
                return StatusCode(result.StatusCode ?? 400, result);

            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddTransaction(CreateTransactionDto dto, int id)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            ServiceResult result = await _transactionsService.AddTransaction(dto, user.Id, id);

            if (!result.Success)
                return StatusCode(result.StatusCode ?? 400, result);

            return Ok(result);
        }
    }
}
