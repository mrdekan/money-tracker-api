using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Data;

namespace money_tracker.Infrastructure.Repositories
{
    public class TransactionsRepository : BaseRepository<Transaction>
    {
        private readonly ApplicationDbContext _context;
        public TransactionsRepository(ApplicationDbContext context) : base(context, context.Transactions)
        {
            _context = context;
        }
    }
}
