using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Utilities.Generics
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;
        protected DbSet<TEntity> _set;

        protected Repository(DbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            // await: Asenkron metotların kullandığı sıraya alma ön ekidir. Bu sayede metot işlem sırasına dahil edilir.
            await _set.AddAsync(entity);
        }

        public async Task CreateManyAsync(IEnumerable<TEntity> entities)
        {
            await _set.AddRangeAsync(entities);
        }

        public async Task<TEntity?> FindByIdAsync(object entityKey)
        {
            return await _set.FindAsync(entityKey);
        }

        public async Task<TEntity?> FindFirstAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression != null ? await _set.FirstOrDefaultAsync(expression) : await _set.FirstOrDefaultAsync();
        }

        public async Task<IQueryable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>>? expression = null, params string[] includes)
        {
            IQueryable<TEntity> data = expression != null ? _set.Where(expression) : _set;

            foreach (var include in includes)
            {
                data = data.Include(include);
            }

            // Task.Run: async olarak çalışmasını istediğimi non-async yapıları dönüştürür.
            return await Task.Run(() => data);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _set.Update(entity));
        }

        public async Task UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _set.UpdateRange(entities));
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _set.Remove(entity));
        }

        public async Task DeleteManyAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _set.RemoveRange(entities));
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression != null ? await _set.AnyAsync(expression) : await _set.AnyAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression != null ? await _set.CountAsync(expression) : await _set.CountAsync();
        }
    }
}
