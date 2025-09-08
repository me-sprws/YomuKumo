using Livetta.Application.Contracts;
using Livetta.Application.DTO.Residents;
using Livetta.Application.Extensions.DTOs;
using Livetta.Domain.Entities;
using Livetta.Domain.Repositories;

namespace Livetta.Application.Services;

public class ResidentService(IResidentRepository residentRepository) : IResidentService
{
    public async Task<ResidentReadDto?> Find(Guid id, CancellationToken ctk = default)
    {
        var query = residentRepository.Get(new(
            IncludeContacts: true, 
            IncludeApartments: false, 
            AsNoTracking: false));

        return await residentRepository.FirstOrDefaultAsync(query, id, ctk) is not { } resident
            ? null
            : resident.ToDto();
    }

    public async Task<ResidentReadDto> Create(ResidentCreateDto request, CancellationToken ctk = default)
    {
        Resident resident = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Contacts = new()
            {
                Phone = request.Phone,
                Email = request.Email
            }
        };

        await residentRepository.AddAsync(resident, ctk);
        await residentRepository.UnitOfWork.SaveChangesAsync(ctk);

        return resident.ToDto();
    }
}