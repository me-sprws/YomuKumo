using Livetta.Application.Contracts;
using Livetta.Application.DTO.Residents;
using Livetta.Application.Extensions.DTOs;
using Livetta.Domain.Entities;
using Livetta.Domain.Repositories;

namespace Livetta.Application.Services;

public sealed class ResidentService(IResidentRepository residentRepository) : IResidentService
{
    public async Task<ResidentReadDto?> FindAsync(Guid id, CancellationToken ctk = default)
    {
        var query = residentRepository.Get(new(
            IncludeContacts: true, 
            IncludeApartments: false, 
            IncludeResidentApartments: true, 
            AsNoTracking: true));

        return await residentRepository.FirstOrDefaultAsync(query, id, ctk) is not { } resident
            ? null
            : resident.ToDto();
    }

    public async Task<List<ResidentReadDto>> GetAll(CancellationToken ctk = default)
    {
        var query = residentRepository.Get(new(
            IncludeContacts: true, 
            IncludeApartments: false, 
            IncludeResidentApartments: true, 
            AsNoTracking: true));

        return (await residentRepository.ToListAsync(query, ctk)).Select(x => x.ToDto()).ToList();
    }

    public async Task<ResidentReadDto> CreateAsync(ResidentCreateDto request, CancellationToken ctk = default)
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