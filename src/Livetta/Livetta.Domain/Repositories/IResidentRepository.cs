using Livetta.Domain.Entities;

namespace Livetta.Domain.Repositories;

public interface IResidentRepository
{
    Task<Resident?> Find(Guid id, CancellationToken ctk = default);
    Task Create(Resident resident, CancellationToken ctk = default);
    Task<List<Resident>> GetAll(CancellationToken ctk = default);
}