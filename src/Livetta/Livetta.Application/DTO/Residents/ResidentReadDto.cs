using Livetta.Application.DTO.Contacts;
using Livetta.Domain.ValueObjects;

namespace Livetta.Application.DTO.Residents;

public record ResidentReadDto(Guid Id, string FullName, ContactsReadDto? Contacts, ResidentApartmentReadDto[] Apartments);

public record ResidentApartmentReadDto(Guid ApartmentId, ResidentRole Role);
