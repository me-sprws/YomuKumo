using Livetta.Application.DTO.Apartments;
using Livetta.Application.DTO.Contacts;
using Livetta.Domain.ValueObjects;

namespace Livetta.Application.DTO.Residents;

public abstract record ResidentReadDtoBase(Guid Id, string FullName, ContactsReadDto? Contacts);

public record ResidentListReadDto(Guid Id, string FullName, ContactsReadDto? Contacts, ResidentApartmentReadDto[] Apartments) 
    : ResidentReadDtoBase(Id, FullName, Contacts);

public record ResidentIdReadDto(Guid Id, string FullName, ContactsReadDto? Contacts, ResidentApartmentFullReadDto[] Apartments) 
    : ResidentReadDtoBase(Id, FullName, Contacts);

public abstract record ResidentApartmentReadDtoBase(ResidentRole Role);

public record ResidentApartmentReadDto(Guid ApartmentId, ResidentRole Role) : ResidentApartmentReadDtoBase(Role);

public record ResidentApartmentFullReadDto(ApartmentReadDto Apartment, ResidentRole Role) : ResidentApartmentReadDtoBase(Role);
