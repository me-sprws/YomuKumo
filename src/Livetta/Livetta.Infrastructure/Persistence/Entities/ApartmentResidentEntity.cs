using Livetta.Domain.Contracts;
using Livetta.Domain.Entities;
using Livetta.Domain.ValueObjects;

namespace Livetta.Infrastructure.Persistence.Entities;

public class ApartmentResidentEntity : Entity
{
    public Guid ApartmentId { get; set; }
    public Apartment Apartment { get; set; }

    public Guid ResidentId { get; set; }
    public Resident Resident { get; set; }

    public ResidentRole Role { get; set; }
}