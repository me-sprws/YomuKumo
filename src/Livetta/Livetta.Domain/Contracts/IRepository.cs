namespace Livetta.Domain.Contracts;

public interface IRepository<T> where T : Entity
{
    Task<List<T>> Get(Func<IQueryable<T>, IQueryable<T>> query, CancellationToken ctk = default);
    Task Create(T entity, CancellationToken ctk = default);
    Task Update(T entity, CancellationToken ctk = default);
    Task Delete(T entity, CancellationToken ctk = default);
}