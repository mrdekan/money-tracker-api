using Microsoft.EntityFrameworkCore;
using money_tracker.Domain.Interfaces;
using money_tracker.Infrastructure.Data;

namespace money_tracker.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> where T : class, IDbStorable
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> Collection => _context.Set<T>();
        public BaseRepository(ApplicationDbContext context, DbSet<T> collection)
        {
            _context = context;
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public virtual async Task Add(T obj)
        {
            Collection.Add(obj);
            await Save();
        }

        public virtual async Task Update(T obj)
        {
            Collection.Update(obj);
            await Save();
        }

        public virtual async Task Delete(T obj)
        {
            Collection.Remove(obj);
            await Save();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await Collection.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await Collection.FindAsync(id);
        }

        public virtual async Task<long> GetCount()
        {
            try
            {
                return await Collection.CountAsync();
            }
            catch
            {
                return 0;
            }
        }
    }
}
