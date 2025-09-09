using Livetta.Domain.Entities;
using Livetta.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Livetta.Infrastructure.Persistence.Repositories;

public class ResidentRepository(LivettaDbContext dbContext) : Repository<Resident>(dbContext), IResidentRepository
{
    public IQueryable<Resident> Get(GetResidentOptions options)
    {
        var builder = QueryableSet;

        if (options.IncludeResidentApartments)
        {
            builder = QueryableSet
                .Include(x => x.Apartments);
        }
        
        if (options.IncludeApartments)
        {
            builder = QueryableSet
                .Include(x => x.Apartments)
                .ThenInclude(x => x.Apartment);
        }

        if (options.IncludeContacts)
        {
            builder = builder.Include(x => x.Contacts);
        }

        if (options.AsNoTracking)
        {
            builder = builder.AsNoTracking();
        }

        return builder;
    }
}