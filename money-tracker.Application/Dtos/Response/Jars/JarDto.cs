using money_tracker.Domain.Entities;

namespace money_tracker.Application.Dtos.Response.Jars
{
    public class JarDto
    {
        public JarDto(Jar jar)
        {
            Name = jar.Name;
            Balance = jar.Balance;
            Target = jar.Target;
            Currency = jar.TargetCurrency.CC;
            Progress = (double)Math.Min(100, (Balance / Target) * 100);
        }

        public string Name { get; set; }
        public decimal Balance { get; set; }
        public double Progress { get; set; }
        public decimal Target { get; set; }
        public string Currency { get; set; }

    }
}
