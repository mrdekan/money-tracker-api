using money_tracker.Infrastructure.Data;

namespace money_tracker.Infrastructure.Services
{
    public class DbTransaction
    {
        private readonly ApplicationDbContext _dbContext;

        public DbTransaction(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task BeginAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContext.Database.CurrentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContext.Database.CurrentTransaction == null)
            {
                throw new InvalidOperationException("No active transaction to commit.");
            }

            await _dbContext.Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContext.Database.CurrentTransaction == null)
            {
                throw new InvalidOperationException("No active transaction to rollback.");
            }

            await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
        }

        public async ValueTask DisposeAsync()
        {
            if (_dbContext.Database.CurrentTransaction != null)
            {
                await _dbContext.Database.RollbackTransactionAsync();
            }
        }
    }
}
