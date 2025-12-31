using System.Linq.Expressions;

namespace Utilities.Generics
{
    // Asenkron kodlama, yapmak istediğimiz işlemleri paralel (eşzamanlı) olarak yapmamızı sağlar, bu sayede bu işlemler yapılırken diğer işlemler etkilenmez.
    // Task -> void metot olarak çalışır.
    // Task<T> -> return type metot olarak çalışır
    public interface IRepository<TEntity> where TEntity : class
    {
        // CRUD İşlemleri:
        Task CreateAsync(TEntity entity);
        Task CreateManyAsync(IEnumerable<TEntity> entities);

        Task<TEntity?> FindByIdAsync(object entityKey);
        Task<TEntity?> FindFirstAsync(Expression<Func<TEntity, bool>>? expression = null);
        Task<IQueryable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>>? expression = null, params string[] includes);

        Task UpdateAsync(TEntity entity);
        Task UpdateManyAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);
        Task DeleteManyAsync(IEnumerable<TEntity> entities);

        // Kontrol ve Sayma İşlemleri
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null);
    }
}
