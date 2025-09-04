using DotNext;
using Livetta.Core;
using Livetta.Core.Extensions;
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

    public async Task<Optional<Resident>> Find(Guid id, CancellationToken ctk = default)
    {
        return (await _dbContext.Residents
            .AsNoTracking()
            .Include(x => x.Contacts)
            .FirstOrDefaultAsync(x => x.Id == id, ctk)
            .Try()
            .ConfigureAwait(false)
        ).Convert(Map);
    }

    public async Task<Result<Unit>> Create(Resident resident, CancellationToken ctk = default)
    {
        try
        {
            await _dbContext.AddAsync(Map(resident), ctk).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync(ctk).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new(ex);
        }

        return new();
    }

    public async Task<Result<List<Resident>>> GetAll(CancellationToken ctk = default)
    {
        return (await _dbContext.Residents
            .AsNoTracking()
            .Include(x => x.Contacts)
            .ToListAsync(ctk)
            .Try()
            .ConfigureAwait(false)
        ).Convert(x => x.Select(Map).ToList());
    }
    
    static Resident Map(ResidentEntity? source)
    {
        if (source is null) return null;
        
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