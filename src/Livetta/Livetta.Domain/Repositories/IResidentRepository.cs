using Livetta.Domain.Contracts;
using Livetta.Domain.Entities;

namespace Livetta.Domain.Repositories;

public record GetResidentOptions(
    bool IncludeContacts,
    bool IncludeApartments,
    bool IncludeResidentApartments,
    bool AsNoTracking);

public interface IResidentRepository : IRepository<Resident>
{
    IQueryable<Resident> Get(GetResidentOptions options);
}