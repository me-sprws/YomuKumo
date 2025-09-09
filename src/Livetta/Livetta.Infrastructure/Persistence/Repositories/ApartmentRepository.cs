using Livetta.Domain.Entities;
using Livetta.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Livetta.Infrastructure.Persistence.Repositories;

public class ApartmentRepository(LivettaDbContext dbContext) : Repository<Apartment>(dbContext), IApartmentRepository
{
    public IQueryable<Apartment> Get(GetApartmentOptions options)
    {
        var builder = QueryableSet;

        if (options.IncludeResidentApartments)
            builder = builder.Include(x => x.Residents);

        if (options.IncludeResidents)
            builder = builder.Include(x => x.Residents).ThenInclude(x => x.Resident);

        if (options.AsNoTracking)
            builder = builder.AsNoTracking();

        return builder;
    }
}