namespace RankedReadyApi.Business.Service.Interfaces;

public interface IGenericServiceAsync<TEntity, TDto> : IReadServiceAsync<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    Task AddAsync(TEntity dto);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(TDto dto);
}
