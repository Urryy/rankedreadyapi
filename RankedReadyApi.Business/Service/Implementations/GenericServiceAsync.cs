using AutoMapper;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReadyApi.Business.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RankedReadyApi.Business.Service.Implementations;

public class GenericServiceAsync<TEntity, TDto> : ReadServiceAsync<TEntity, TDto>, IGenericServiceAsync<TEntity, TDto>
    where TDto : class
    where TEntity : class
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GenericServiceAsync(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task AddAsync(TDto dto)
    {
        var entity = mapper.Map<TEntity>(dto);
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
