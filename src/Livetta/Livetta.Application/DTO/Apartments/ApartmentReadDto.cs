namespace Livetta.Application.DTO.Apartments;

public record ApartmentReadDto(
    Guid Id, 
    string Address, 
    int Room, 
    int Floor, 
    double Area
);

public record ApartmentResidentDto(Guid ResidentId, Guid Role);