using Livetta.Application.Contracts;
using Livetta.Domain.Entities;
using Livetta.Domain.Repositories;

namespace Livetta.Application.Specification.Residents;

public record GetResidentQuery(
    Guid Id,
    bool IncludeContacts, 
    bool IncludeApartments, 
    bool AsNoTracking
) : IQuery<Resident?>;

public class GetResidentHandler(IResidentRepository repository) : IQueryHandle<GetResidentQuery, Resident?>
{
    public Task<Resident?> HandleAsync(GetResidentQuery query, CancellationToken ctk = default)
    {
        var db = repository.Get(new(query.IncludeContacts, query.IncludeApartments, query.AsNoTracking));
        return repository.FirstOrDefaultAsync(db, query.Id, ctk);
    }
}