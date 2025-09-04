using DotNext;
using Livetta.Core;
using Livetta.Domain.Contracts;
using Livetta.Domain.Entities;

namespace Livetta.Domain.Repositories;

public interface IResidentRepository
{
    Task<Optional<IResident>> Find(Guid id, CancellationToken ctk = default);
    Task<Result<Unit>> Create(IResident resident, CancellationToken ctk = default);
    Task<Result<List<IResident>>> GetAll(CancellationToken ctk = default);
}