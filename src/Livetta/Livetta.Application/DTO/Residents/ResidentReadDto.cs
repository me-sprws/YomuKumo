using Livetta.Application.DTO.Contacts;

namespace Livetta.Application.DTO.Residents;

public record struct ResidentReadDto(Guid Id, string FullName, ContactsReadDto Contacts);
