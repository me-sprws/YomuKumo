namespace Livetta.Application.Contracts;

public interface IQueryHandle<in TQuery, TResult> where TQuery : IQuery<TResult?>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken ctk = default);
}