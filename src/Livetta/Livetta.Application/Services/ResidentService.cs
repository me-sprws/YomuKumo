using Livetta.Application.Contracts;
using Livetta.Application.DTO.Residents;
using Livetta.Application.Extensions.DTOs;
using Livetta.Domain.Entities;
using Livetta.Domain.Repositories;

namespace Livetta.Application.Services;

public sealed class ResidentService(IResidentRepository residentRepository) : IResidentService
{
    public async Task<ResidentIdReadDto?> FindAsync(Guid id, CancellationToken ctk = default)
    {
        var query = residentRepository.Get(new(
            IncludeContacts: true, 
            IncludeApartments: true,
            IncludeResidentApartments: true, 
            AsNoTracking: true));

        return await residentRepository.FirstOrDefaultAsync(query, id, ctk) is not { } resident
            ? null
            : resident.ToIdDto();
    }

    public async Task<List<ResidentListReadDto>> GetAllAsync(CancellationToken ctk = default)
    {
        var query = residentRepository.Get(new(
            IncludeContacts: true, 
            IncludeResidentApartments: true, 
            AsNoTracking: true));

        return (await residentRepository.ToListAsync(query, ctk)).Select(x => x.ToListDto()).ToList();
    }

    public async Task<ResidentListReadDto> CreateAsync(ResidentCreateDto request, CancellationToken ctk = default)
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

        return resident.ToListDto();
    }
}