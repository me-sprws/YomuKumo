using Livetta.Domain.Contracts;
using Livetta.Domain.ValueObjects;

namespace Livetta.Domain.Entities;

public class ResidentApartment : Entity
{
    public Guid ApartmentId { get; set; }
    public Apartment Apartment { get; set; }
    
    public Guid ResidentId { get; set; }
    public Resident Resident { get; set; }
    
    public ResidentRole Role { get; set; }
}