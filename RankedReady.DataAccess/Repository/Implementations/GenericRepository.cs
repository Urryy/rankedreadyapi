using Microsoft.EntityFrameworkCore;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReadyApi.Common.Context;
using System.Linq.Expressions;


namespace RankedReady.DataAccess.Repository.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDataBaseContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDataBaseContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entityToDelete = await _dbSet.FindAsync(id);

        if(entityToDelete != null)
        {
            _dbSet.Remove(entityToDelete);
        }
    }

    public async Task<List<T?>> GetAllAsync(bool tracked = true)
    {
        IQueryable<T> query = _dbSet;

        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        return await query.ToListAsync();
    }

    public async Task<List<T?>> GetAllByExpressionAsync(Expression<Func<T, bool>> expression, bool tracked = true)
    {
        IQueryable<T> query = _dbSet;

        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        return await query.Where(expression).ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> GetFirstAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.FirstOrDefaultAsync(expression);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }
}
