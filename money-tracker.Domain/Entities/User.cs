using Microsoft.AspNetCore.Identity;
using money_tracker.Domain.Interfaces;

namespace money_tracker.Domain.Entities
{
    public class User : IdentityUser<int>, IDbStorable
    {
        public ICollection<Jar> Jars { get; set; } = new List<Jar>();
        public ICollection<CurrencyBalance> CurrencyBalances { get; set; } = new List<CurrencyBalance>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
