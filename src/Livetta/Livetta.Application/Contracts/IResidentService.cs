using DotNext;
using Livetta.Application.DTO.Residents;

namespace Livetta.Application.Contracts;

public interface IResidentService
{
    Task<Optional<ResidentReadDto>> GetById(Guid id, CancellationToken ctk = default);
    Task<Result<ResidentReadDto[]>> GetAll(CancellationToken ctk = default);
    Task<Result<ResidentReadDto>> Create(ResidentCreateDto request, CancellationToken ctk = default);
}