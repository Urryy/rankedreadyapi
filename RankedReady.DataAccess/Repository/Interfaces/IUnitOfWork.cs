namespace RankedReady.DataAccess.Repository.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
}
