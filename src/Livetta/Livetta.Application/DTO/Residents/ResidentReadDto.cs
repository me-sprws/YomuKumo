using Livetta.Application.DTO.Contacts;

namespace Livetta.Application.DTO.Residents;

public record ResidentReadDto(Guid Id, string FullName, ContactsReadDto? Contacts);
