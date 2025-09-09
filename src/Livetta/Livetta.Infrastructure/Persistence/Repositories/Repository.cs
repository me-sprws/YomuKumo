using Livetta.Domain.Contracts;
using Livetta.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Livetta.Infrastructure.Persistence.Repositories;

public abstract class Repository<TEntity>(LivettaDbContext dbContext) : IRepository<TEntity>
    where TEntity : Entity
{
    public IUnitOfWork UnitOfWork => dbContext;
    public IQueryable<TEntity> QueryableSet => dbContext.Set<TEntity>();
    
    public async Task AddAsync(TEntity entity, CancellationToken ctk = default)
    {
        await dbContext.AddAsync(entity, ctk);
    }

    public Task UpdateAsync(TEntity entity, CancellationToken ctk = default)
    {
        dbContext.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity, CancellationToken ctk = default)
    {
        dbContext.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<bool> AnyAsync(Guid id, CancellationToken ctk = default)
    {
        return QueryableSet.AnyAsync(x => x.Id == id, ctk);
    }

    public Task<TEntity?> FirstOrDefaultAsync(IQueryable<TEntity> query, Guid id, CancellationToken ctk = default)
    {
        return query.FirstOrDefaultAsync(x => x.Id == id, ctk);
    }

    public Task<List<TEntity>> ToListAsync(IQueryable<TEntity> query, CancellationToken ctk = default)
    {
        return query.ToListAsync(ctk);
    }
}