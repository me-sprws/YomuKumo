using Livetta.Domain.Entities;
using Livetta.Domain.Repositories;
namespace Livetta.Infrastructure.Persistence.Repositories;

public class ApartmentRepository(LivettaDbContext dbContext) : Repository<Apartment>(dbContext), IApartmentRepository;