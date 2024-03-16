using System.Linq.Expressions;

namespace RankedReadyApi.Business.Service.Interfaces;

public interface IReadServiceAsync<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<IEnumerable<TDto>> GetAllByExpressionAsync(Expression<Func<TEntity, bool>> expression);
    Task<TDto> GetAsync(Guid id);
    Task<TDto> GetOrDefaultAsync(Guid id);
    Task<TDto> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression);
    Task<int> Count();
}
