using Livetta.Application.DTO.Apartments;

namespace Livetta.Application.Contracts;

public interface IApartmentService
{
    Task<ApartmentReadDto> CreateAsync(ApartmentCreateDto request, CancellationToken ctk = default);
    Task AssignAsync(Guid residentId, Guid apartmentId, ResidentAssignDto request, CancellationToken ctk = default);
}