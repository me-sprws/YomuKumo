using Livetta.Application.DTO.Residents;

namespace Livetta.Application.Contracts;

public interface IResidentService
{
    Task<ResidentReadDto?> Find(Guid id, CancellationToken ctk = default);
    Task<ResidentReadDto> Create(ResidentCreateDto request, CancellationToken ctk = default);
}