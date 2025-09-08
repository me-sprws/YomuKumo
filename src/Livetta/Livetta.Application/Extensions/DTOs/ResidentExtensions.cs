using Livetta.Application.DTO.Residents;
using Livetta.Domain.Entities;

namespace Livetta.Application.Extensions.DTOs;

public static class ResidentExtensions
{
    public static ResidentReadDto ToDto(this Resident resident)
    {
        return new(resident.Id, resident.FirstName + " " + resident.LastName, resident.Contacts.ToDto());
    }
}