using RankedReady.DataAccess.Repository.Interfaces;
using RankedReadyApi.Common.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankedReady.DataAccess.Repository.Implementations;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDataBaseContext _context;
    private bool disposed = false;

    public UnitOfWork(ApplicationDataBaseContext context)
    {
        _context = context;
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class 
        => new GenericRepository<TEntity>(_context);
    
    public async Task SaveChangesAsync() 
        => await _context.SaveChangesAsync();
    
    public void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
