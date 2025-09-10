using Livetta.Application.DTO.Residents;

namespace Livetta.Application.Contracts;

public interface IResidentService
{
    Task<ResidentIdReadDto?> FindAsync(Guid id, CancellationToken ctk = default);
    Task<List<ResidentListReadDto>> GetAll(CancellationToken ctk = default);
    Task<ResidentListReadDto> CreateAsync(ResidentCreateDto request, CancellationToken ctk = default);
}