using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Repositories;

namespace money_tracker.Application.Services
{
    public class CurrenciesService : BackgroundService
    {
        private readonly ILogger<CurrenciesService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly int _delayMinutes;
        private readonly string _bankAPI;

        public CurrenciesService(ILogger<CurrenciesService> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            bool parsedDelay = int.TryParse(configuration["Currency:RefreshRate"], out _delayMinutes);
            if (!parsedDelay)
            {
                _logger.Log(LogLevel.Warning, "Delay is not parsed");
                _delayMinutes = 60 * 12; //12 hours
            }

            _bankAPI = configuration["Currency:API"];
            if (string.IsNullOrEmpty(_bankAPI))
            {
                throw new Exception("Currency API not found in appsettings.json");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Updating currencies.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using HttpClient client = new HttpClient();

                    HttpResponseMessage response = await client.GetAsync(_bankAPI);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    List<CurrencyRate>? rates = JsonSerializer.Deserialize<List<CurrencyRate>>(jsonResponse);

                    if (rates != null)
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var repository = scope.ServiceProvider.GetRequiredService<CurrenciesRepository>();
                            if (repository != null)
                            {
                                await repository.UpdateCurrencies(rates.Select(r => new Currency() { CC = r.cc, Rate = r.rate }).ToList());
                            }
                            else
                            {
                                _logger.Log(LogLevel.Error, "Error getting CurrencyRepository");
                            }
                        }
                    }
                    else
                    {
                        _logger.Log(LogLevel.Error, "Error getting currencies");
                    }
                    await Task.Delay(TimeSpan.FromMinutes(_delayMinutes), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while running the scheduler task.");
                }
            }
        }
    }

    public class CurrencyRate
    {
        public int r030 { get; set; }
        public string txt { get; set; } = string.Empty;
        public double rate { get; set; }
        public string cc { get; set; } = string.Empty;
        public string exchangedate { get; set; } = string.Empty;
    }
}
