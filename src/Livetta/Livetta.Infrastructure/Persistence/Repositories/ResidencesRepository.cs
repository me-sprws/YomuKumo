using Livetta.Domain.Entities;
using Livetta.Domain.Repositories;

namespace Livetta.Infrastructure.Persistence.Repositories;

/// <summary>
/// ResidentApartment.
/// </summary>
/// <param name="dbContext"></param>
public class ResidencesRepository(LivettaDbContext dbContext) : Repository<ResidentApartment>(dbContext), IResidencesRepository;
