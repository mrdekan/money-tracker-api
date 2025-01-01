using money_tracker.Application.Interfaces;
using money_tracker.Domain.Entities;

namespace money_tracker.Application.Dtos.Response.Jars
{
    public class JarDto
    {
        public JarDto(Jar entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Balance = 0;
            TargetCurrency = entity.TargetCurrency.CC;
            Target = entity.Target;
            Progress = 0;
        }

        public JarDto(
            Jar entity,
            Dictionary<string, decimal> currencies,
            ICurrenciesService currencyService
        )
        {
            Id = entity.Id;
            Name = entity.Name;
            Balance = 0;
            TargetCurrency = entity.TargetCurrency.CC;
            foreach (var currency in currencies.Keys)
            {
                if (currency == TargetCurrency)
                {
                    Balance += currencies[currency];
                }
                else
                {
                    Balance += currencyService
                        .Convert(currencies[currency], currency, TargetCurrency)
                        .Result;
                }
            }
            Target = entity.Target;
            Progress = (double)Math.Min(100, (Balance / Target) * 100);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public double Progress { get; set; }
        public decimal Target { get; set; }
        public string TargetCurrency { get; set; }
    }
}
