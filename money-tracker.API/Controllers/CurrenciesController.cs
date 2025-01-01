using Microsoft.AspNetCore.Mvc;
using money_tracker.Application.Interfaces;
using money_tracker.Application.Services;

namespace money_tracker.API.Controllers
{
    [Route("api/v1/currencies")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrenciesService _currenciesService;

        public CurrenciesController(ICurrenciesService currenciesService)
        {
            _currenciesService = currenciesService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            ServiceResult result = await _currenciesService.GetAll();
            return Ok(result);
        }
    }
}
