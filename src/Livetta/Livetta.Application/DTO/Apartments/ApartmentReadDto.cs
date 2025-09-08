namespace Livetta.Application.DTO.Apartments;

public readonly record struct ApartmentReadDto(
    Guid Id, 
    string Address, 
    int Room, 
    int Floor, 
    double Area, 
    ApartmentResidentDto[] Residents
);

public readonly record struct ApartmentResidentDto(Guid ResidentId, Guid Role);