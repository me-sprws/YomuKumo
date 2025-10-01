using Livetta.Application.DTO.Apartments;
using Livetta.Domain.Entities;

namespace Livetta.Application.Extensions.DTOs;

public static class ApartmentExtensions
{
    public static ApartmentReadDto ToDto(this Apartment apartment)
    {
        return new(apartment.Id, apartment.Address, apartment.Room, apartment.Floor, apartment.Area);
    }
}