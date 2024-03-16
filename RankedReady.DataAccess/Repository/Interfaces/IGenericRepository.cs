using System.Linq.Expressions;

namespace RankedReady.DataAccess.Repository.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<T> GetByIdAsync(Guid id);
    Task<T> GetFirstAsync(Expression<Func<T, bool>> expression);
    Task<List<T?>> GetAllByExpressionAsync(Expression<Func<T, bool>> expression, bool tracked = true);
    Task<List<T?>> GetAllAsync(bool tracked = true);
    Task UpdateAsync(T entity);
    Task DeleteByIdAsync(Guid id);
}
