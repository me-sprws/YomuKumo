namespace Livetta.Domain.Repositories;

public interface IUnitOfWork
{
    Task<IDisposable> BeginTransactionAsync(CancellationToken ctk = default);
    Task CommitTransactionAsync(CancellationToken ctk = default);
    Task<int> SaveChangesAsync(CancellationToken ctk = default);
}