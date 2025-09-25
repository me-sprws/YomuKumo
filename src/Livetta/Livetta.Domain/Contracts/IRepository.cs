using Livetta.Domain.Repositories;

namespace Livetta.Domain.Contracts;

public interface IRepository<TEntity> where TEntity : Entity
{
    IUnitOfWork UnitOfWork { get; }
    IQueryable<TEntity> QueryableSet { get; }
    
    Task AddAsync(TEntity entity, CancellationToken ctk = default);
    Task UpdateAsync(TEntity entity, CancellationToken ctk = default);
    Task DeleteAsync(TEntity entity, CancellationToken ctk = default);
    Task<bool> AnyAsync(Guid id, CancellationToken ctk = default);
    Task<bool> AnyAsync(IQueryable<TEntity> query, CancellationToken ctk = default);
    Task<TEntity?> FirstOrDefaultAsync(IQueryable<TEntity> query, Guid id, CancellationToken ctk = default);
    Task<List<TEntity>> ToListAsync(IQueryable<TEntity> query, CancellationToken ctk = default);
}