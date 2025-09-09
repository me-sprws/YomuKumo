using Livetta.Application.DTO.Residents;

namespace Livetta.Application.Contracts;

public interface IResidentService
{
    Task<ResidentReadDto?> FindAsync(Guid id, CancellationToken ctk = default);
    Task<List<ResidentReadDto>> GetAll(CancellationToken ctk = default);
    Task<ResidentReadDto> CreateAsync(ResidentCreateDto request, CancellationToken ctk = default);
}