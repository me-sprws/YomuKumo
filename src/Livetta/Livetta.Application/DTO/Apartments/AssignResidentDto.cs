using Livetta.Domain.ValueObjects;

namespace Livetta.Application.DTO.Apartments;

public readonly record struct AssignResidentDto(Guid ResidentId, Guid ApartmentId, ResidentRole Role);