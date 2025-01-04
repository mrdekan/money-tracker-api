using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace money_tracker.Application.Services
{
    public class ConfigManager
    {
        public string BaseCurrency { get; private set; }
        public int CurrencyUpdateDelayMinutes { get; private set; }
        public string BankAPI { get; private set; }
        public string JwtKey { get; set; }
        public string JwtAudience { get; set; }
        public string JwtIssuer { get; set; }
        private string _env;

        public ConfigManager(
            IConfiguration configuration,
            ILogger<CurrenciesUpdaterService> logger,
            IHostEnvironment env
        )
        {
            _env = env.EnvironmentName;

            BaseCurrency = configuration["Currency:Default"];
            ValidateString(BaseCurrency, nameof(BaseCurrency));

            BankAPI = configuration["Currency:API"];
            ValidateString(BankAPI, nameof(BankAPI));

            JwtKey = configuration["Jwt:Key"];
            ValidateString(JwtKey, nameof(JwtKey));

            JwtAudience = configuration["Jwt:Issuer"];
            ValidateString(JwtAudience, nameof(JwtAudience));

            JwtIssuer = configuration["Jwt:Issuer"];
            ValidateString(JwtIssuer, nameof(JwtIssuer));

            bool parsedDelay = int.TryParse(configuration["Currency:RefreshRate"], out int parsed);
            if (parsedDelay)
            {
                CurrencyUpdateDelayMinutes = parsed;
            }
            else
            {
                logger.Log(LogLevel.Warning, "Delay is not parsed");
                CurrencyUpdateDelayMinutes = 60 * 12; //12 hours
            }
        }

        private void ValidateString(string? value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception($"{name} not found in appsettings.{_env}.json.");
            }
        }
    }
}
