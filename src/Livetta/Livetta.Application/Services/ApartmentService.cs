using Livetta.Application.Contracts;
using Livetta.Application.DTO.Apartments;
using Livetta.Application.Extensions.DTOs;
using Livetta.Domain.Entities;
using Livetta.Domain.Repositories;

namespace Livetta.Application.Services;

public sealed class ApartmentService(
    IResidentRepository residentRepository,
    IApartmentRepository apartmentRepository,
    IResidencesRepository residencesRepository
) : IApartmentService
{
    public async Task<ApartmentReadDto> CreateAsync(ApartmentCreateDto request, CancellationToken ctk = default)
    {
        Apartment apartment = new()
        {
            Address = request.Address,
            Area = request.Area,
            Floor = request.Floor,
            Room = request.Room
        };

        await apartmentRepository.AddAsync(apartment, ctk);
        await apartmentRepository.UnitOfWork.SaveChangesAsync(ctk);

        return apartment.ToDto();
    }

    public async Task AssignAsync(Guid residentId, Guid apartmentId, ResidentAssignDto request, CancellationToken ctk = default)
    {
        var anyApartment = apartmentRepository.AnyAsync(apartmentId, ctk);
        var anyResident = residentRepository.AnyAsync(residentId, ctk);

        await Task.WhenAll(anyApartment, anyResident);

        if (!await anyApartment && !await anyResident)
            throw new InvalidOperationException("Entries not exists.");
        
        ResidentApartment ra = new()
        {
            Role = request.Role,
            ResidentId = residentId,
            ApartmentId = apartmentId
        };

        await residencesRepository.AddAsync(ra, ctk);
        await residencesRepository.UnitOfWork.SaveChangesAsync(ctk);
    }

    public async Task<List<ApartmentReadDto>> GetAllAsync(CancellationToken ctk = default)
    {
        var query = apartmentRepository.Get(new(AsNoTracking: true));
        return (await apartmentRepository.ToListAsync(query, ctk)).Select(x => x.ToDto()).ToList();
    }
}