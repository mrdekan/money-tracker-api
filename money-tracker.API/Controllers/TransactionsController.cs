using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using money_tracker.Application.Dtos.Requests.Transactions;
using money_tracker.Application.Interfaces;
using money_tracker.Application.Services;
using money_tracker.Domain.Entities;

namespace money_tracker.API.Controllers
{
    [Route("api/v1/transactions")]
    [ApiController]
    [Authorize]
    public class TransactionsController(
        ITransactionsService transactionsService,
        UserManager<User> userManager
    ) : BaseController(userManager)
    {
        private readonly ITransactionsService _transactionsService = transactionsService;

        [HttpPost("")]
        public async Task<IActionResult> AddTransaction(CreateTransactionDto dto)
        {
            var userId = await GetUserIdAsync();

            ServiceResult result = await _transactionsService.AddTransaction(dto, userId);
            return GenerateResponse(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetMany([FromQuery] TransactionQuery query)
        {
            var userId = await GetUserIdAsync();

            ServiceResult result = await _transactionsService.GetMany(query, userId);
            return GenerateResponse(result);
        }
    }
}
