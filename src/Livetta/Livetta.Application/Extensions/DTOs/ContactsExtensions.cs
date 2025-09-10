using Livetta.Application.DTO.Contacts;
using Livetta.Domain.Entities;

namespace Livetta.Application.Extensions.DTOs;

public static class ContactsExtensions
{
    public static ContactsReadDto? ToDto(this Contacts? contacts)
    {
        if (contacts is null) return null;

        return new(contacts.Phone, contacts.Email);
    }
}