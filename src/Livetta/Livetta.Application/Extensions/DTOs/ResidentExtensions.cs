using Livetta.Application.DTO.Residents;
using Livetta.Domain.Entities;

namespace Livetta.Application.Extensions.DTOs;

public static class ResidentExtensions
{
    public static ResidentIdReadDto ToIdDto(this Resident resident)
    {
        return new(
            resident.Id, 
            resident.FirstName + " " + resident.LastName, 
            resident.Contacts.ToDto(),
            resident.Apartments.Select(a => a.ToFullDto()).ToArray());
    }
    
    public static ResidentListReadDto ToListDto(this Resident resident)
    {
        return new(
            resident.Id, 
            resident.FirstName + " " + resident.LastName, 
            resident.Contacts.ToDto(),
            resident.Apartments.Select(ToDto).ToArray());
    }

    public static ResidentApartmentReadDto ToDto(this ResidentApartment ra)
    {
        return new(ra.ApartmentId, ra.Role);
    }
    
    public static ResidentApartmentFullReadDto ToFullDto(this ResidentApartment ra)
    {
        return new(ra.Apartment.ToDto(), ra.Role);
    }
}