using DotNext;
using Livetta.Core;
using Livetta.Domain.Entities;

namespace Livetta.Domain.Repositories;

public interface IResidentRepository
{
    Task<Optional<Resident>> Find(Guid id, CancellationToken ctk = default);
    Task<Result<Unit>> Create(Resident resident, CancellationToken ctk = default);
    Task<Result<List<Resident>>> GetAll(CancellationToken ctk = default);
}