using AutoMapper;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReadyApi.Business.Service.Interfaces;

namespace RankedReadyApi.Business.Service.Implementations;

public class GenericServiceAsync<TEntity, TDto> : ReadServiceAsync<TEntity, TDto>, IGenericServiceAsync<TEntity, TDto>
    where TDto : class
    where TEntity : class
{
    private readonly IUnitOfWork unitOfWork;
    protected readonly IMapper mapper;

    public GenericServiceAsync(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task AddAsync(TEntity entity)
    {
        await unitOfWork.Repository<TEntity>().AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await unitOfWork.Repository<TEntity>().DeleteByIdAsync(id);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(TDto dto)
    {
        var entity = mapper.Map<TEntity>(dto);
        await unitOfWork.Repository<TEntity>().UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
    }
}
