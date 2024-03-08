using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RankedReadyApi.Business.Service.Interfaces;

public interface IReadServiceAsync<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<IEnumerable<TDto>> GetAllByExpressionAsync(Expression<Func<TEntity, bool>> expression);
    Task<TDto> GetAsync(Guid id);
    Task<TDto> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression);
}
