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

public class ReadServiceAsync<TEntity, TDto> : IReadServiceAsync<TEntity, TDto>
    where TDto : class
    where TEntity : class
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ReadServiceAsync(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<TDto>> GetAllAsync()
    {
        try
        {
            var entities = await unitOfWork.Repository<TEntity>().GetAllAsync();

            if (entities.Any())
            {
                return mapper.Map<IEnumerable<TDto>>(entities);
            }
            else
            {
                throw new Exception($"No {typeof(TDto).Name}s were found");
            }
        }
        catch (Exception ex)
        {
            var message = $"Error retrieving all {typeof(TDto).Name}s";
            throw new Exception(message, ex);
        }
    }

    public async Task<IEnumerable<TDto>> GetAllByExpressionAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entities = await unitOfWork.Repository<TEntity>().GetAllByExpressionAsync(expression);

            if (entities.Any())
            {
                return mapper.Map<IEnumerable<TDto>>(entities);
            }
            else
            {
                throw new Exception($"No {typeof(TDto).Name}s were found");
            }
        }
        catch (Exception ex)
        {
            var message = $"Error retrieving all {typeof(TDto).Name}s";
            throw new Exception(message, ex);
        }
    }

    public async Task<TDto> GetAsync(Guid id)
    {
        try
        {
            var entity = await unitOfWork.Repository<TEntity>().GetByIdAsync(id);

            if(entity is null)
            {
                throw new Exception($"Entity with ID {id} not found.");
            }

            return mapper.Map<TDto>(entity);
        }
        catch (Exception ex)
        {
            var message = $"Error retrieving {typeof(TDto).Name} with Id: {id}";
            throw new Exception(message, ex);
        }
    }

    public async Task<TDto> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = await unitOfWork.Repository<TEntity>().GetFirstAsync(expression);

            if (entity is null)
            {
                throw new Exception($"Entity does not found.");
            }

            return mapper.Map<TDto>(entity);
        }
        catch (Exception ex)
        {
            var message = $"Error retrieving {typeof(TDto).Name}";
            throw new Exception(message, ex);
        }
    }
}
