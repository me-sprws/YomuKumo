using Livetta.Domain.Contracts;
using Livetta.Domain.Entities;

namespace Livetta.Domain.Repositories;

public record GetResidentOptions(
    bool IncludeContacts = false,
    bool IncludeApartments = false,
    bool IncludeResidentApartments = false,
    bool AsNoTracking = false
);

public interface IResidentRepository : IRepository<Resident>
{
    IQueryable<Resident> Get(GetResidentOptions options);
}