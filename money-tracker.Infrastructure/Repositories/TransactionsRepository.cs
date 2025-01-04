using Microsoft.EntityFrameworkCore;
using money_tracker.Domain.Entities;
using money_tracker.Infrastructure.Data;

namespace money_tracker.Infrastructure.Repositories
{
    public class TransactionsRepository : BaseRepository<Transaction>
    {
        private readonly ApplicationDbContext _context;

        public TransactionsRepository(ApplicationDbContext context)
            : base(context, context.Transactions)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetMany(
            int offset,
            int limit,
            int userId,
            int? storeId
        )
        {
            var query = _context
                .Transactions.Include(t => t.Source)
                .Include(t => t.Source.Currency)
                .Where(t => t.UserId == userId);

            if (storeId.HasValue)
            {
                query = query.Where(t => t.Source.StoreId == storeId.Value);
            }

            var transactions = await query.Skip(offset).Take(limit).ToListAsync();

            return transactions;
        }
    }
}
