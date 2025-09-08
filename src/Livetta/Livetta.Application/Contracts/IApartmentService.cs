using DotNext;
using Livetta.Application.DTO.Apartments;
using Livetta.Core;

namespace Livetta.Application.Contracts;

public interface IApartmentService
{
    Task<Result<ApartmentReadDto>> Create(ApartmentCreateDto request, CancellationToken ctk = default);
    Task<Result<Unit>> AssignResident(AssignResidentDto request, CancellationToken ctk = default);
}