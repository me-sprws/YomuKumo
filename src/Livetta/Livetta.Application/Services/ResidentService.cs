using Livetta.Application.Contracts;
using Livetta.Application.DTO.Residents;
using Livetta.Domain.Entities;
using Livetta.Domain.Repositories;
using Livetta.Domain.ValueObjects;

namespace Livetta.Application.Services;

public class ResidentService(IResidentRepository residents) : IResidentService
{
    public async Task<ResidentReadDto?> GetById(Guid id, CancellationToken ctk = default)
    {
        if (await residents.Find(id, ctk).ConfigureAwait(false) is { } resident)
            return Map(resident);

        return null;
    }

    public async Task<ResidentReadDto[]> GetAll(CancellationToken ctk = default)
    {
        var entities = await residents.GetAll(ctk).ConfigureAwait(false);
        return entities.Select(Map).ToArray();
    }

    public async Task<ResidentReadDto> Create(ResidentCreateDto request, CancellationToken ctk = default)
    {
        var resident = Map(request);
        await residents.Create(resident, ctk).ConfigureAwait(false);

        return Map(resident);
    }

    static ResidentReadDto Map(Resident resident)
    {
        var contacts = resident.Contacts;
        return new(resident.Id, resident.FullName, new(contacts.FirstName, contacts.LastName, contacts.Phone.ToString()));
    }
    
    static Resident Map(ResidentCreateDto create)
    {
        return new(new Contacts(create.FirstName, create.LastName, Phone.CreateValid(create.Phone)));
    }
}