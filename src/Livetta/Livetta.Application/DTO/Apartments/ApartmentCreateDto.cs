namespace Livetta.Application.DTO.Apartments;

public readonly record struct ApartmentCreateDto(string Address, int Room, int Floor, float Area);