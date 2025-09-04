using Livetta.Domain.Entities;
using Livetta.Domain.Repositories;
using Livetta.Domain.ValueObjects;
using Livetta.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livetta.Infrastructure.Persistence.Repositories;

public class ResidentRepository : IResidentRepository
{
    readonly LivettaDbContext _dbContext;

    public ResidentRepository(LivettaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Resident?> Find(Guid id, CancellationToken ctk = default)
    {
        var resident = await _dbContext.Residents
            .AsNoTracking()
            .Include(x => x.Contacts)
            .FirstOrDefaultAsync(x => x.Id == id, ctk)
            .ConfigureAwait(false);

        return resident is null ? null : Map(resident);
    }

    public async Task Create(Resident resident, CancellationToken ctk = default)
    {
        await _dbContext.AddAsync(Map(resident), ctk).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(ctk).ConfigureAwait(false);
    }

    public async Task<List<Resident>> GetAll(CancellationToken ctk = default)
    {
        var residents = await _dbContext.Residents
            .AsNoTracking()
            .Include(x => x.Contacts)
            .ToListAsync(ctk)
            .ConfigureAwait(false);

        return residents.Select(Map).ToList();
    }
    
    static Resident Map(ResidentEntity source)
    {
        var contacts = source.Contacts;
        return new(new Contacts(
            contacts.FirstName,
            contacts.LastName,
            Phone.CreateValidOrNull(contacts.Phone)))
        {
            Id = source.Id
        };
    }
    
    static ResidentEntity Map(Resident source) =>
        new()
        {
            Id = source.Id,
            Contacts = new()
            {
                FirstName = source.Contacts.FirstName,
                LastName = source.Contacts.LastName,
                Phone = source.Contacts.Phone.ToString(),
            }
        };
}