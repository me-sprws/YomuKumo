using DotNext;
using Livetta.Application.Contracts;
using Livetta.Application.DTO.Residents;
using Livetta.Domain.Contracts;
using Livetta.Domain.Entities;
using Livetta.Domain.Repositories;
using Livetta.Domain.ValueObjects;

namespace Livetta.Application.Services;

public class ResidentService(IResidentRepository residents) : IResidentService
{
    public async Task<Optional<ResidentReadDto>> GetById(Guid id, CancellationToken ctk = default)
    {
        return (await residents.Find(id, ctk).ConfigureAwait(false)).Convert(Map);
    }

    public async Task<Result<ResidentReadDto[]>> GetAll(CancellationToken ctk = default)
    {
        return (await residents.GetAll(ctk).ConfigureAwait(false))
            .Convert(x => x.Select(Map).ToArray());
    }

    public async Task<Result<ResidentReadDto>> Create(ResidentCreateDto request, CancellationToken ctk = default)
    {
        var resident = Map(request);
        
        return (await residents.Create(resident, ctk).ConfigureAwait(false))
            .Convert(x => Map(resident));
    }

    static ResidentReadDto Map(IResident resident)
    {
        var contacts = resident.Contacts;
        return new(resident.Id, resident.FullName, new(contacts.FirstName, contacts.LastName, contacts.Phone.ToString()));
    }
    
    static IResident Map(ResidentCreateDto create)
    {
        return new Resident(new Contacts(create.FirstName, create.LastName, Phone.CreateValid(create.Phone)));
    }
}