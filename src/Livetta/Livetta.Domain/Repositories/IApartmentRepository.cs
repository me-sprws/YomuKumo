using Livetta.Domain.Contracts;
using Livetta.Domain.Entities;

namespace Livetta.Domain.Repositories;

public record GetApartmentOptions(
    bool IncludeResidentApartments = false,
    bool IncludeResidents = false,
    bool AsNoTracking = false
);

public interface IApartmentRepository : IRepository<Apartment>
{
    IQueryable<Apartment> Get(GetApartmentOptions options);
}