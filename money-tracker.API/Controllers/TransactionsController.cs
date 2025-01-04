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
    public class TransactionsController : BaseController
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(
            ITransactionsService transactionsService,
            UserManager<User> userManager
        )
            : base(userManager)
        {
            _transactionsService = transactionsService;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddTransaction(CreateTransactionDto dto)
        {
            var user = await GetCurrentUserAsync();
            Console.WriteLine(user?.Id);
            if (user == null)
            {
                return Unauthorized();
            }

            ServiceResult result = await _transactionsService.AddTransaction(dto, user.Id);

            if (!result.Success)
                return StatusCode(result.StatusCode ?? 400, result);

            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetMany([FromQuery] TransactionQuery query)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            ServiceResult result = await _transactionsService.GetMany(query, user.Id);

            if (!result.Success)
                return StatusCode(result.StatusCode ?? 400, result);

            return Ok(result);
        }
    }
}
