using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankedReadyApi.Business.Service.Interfaces;

public interface IGenericServiceAsync<TEntity, TDto> : IReadServiceAsync<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    Task AddAsync(TDto dto);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(TDto dto);
}
